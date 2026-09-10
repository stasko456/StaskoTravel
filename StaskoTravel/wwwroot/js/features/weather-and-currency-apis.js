const weatherCodeMap = {
    0: "Clear sky",
    1: "Mainly clear",
    2: "Partly cloudy",
    3: "Overcast",
    45: "Fog",
    48: "Depositing rime fog",
    51: "Light drizzle",
    53: "Moderate drizzle",
    55: "Dense drizzle",
    56: "Light freezing drizzle",
    57: "Dense freezing drizzle",
    61: "Slight rain",
    63: "Moderate rain",
    65: "Heavy rain",
    66: "Light freezing rain",
    67: "Heavy freezing rain",
    71: "Slight snow fall",
    73: "Moderate snow fall",
    75: "Heavy snow fall",
    77: "Snow grains",
    80: "Slight rain showers",
    81: "Moderate rain showers",
    82: "Violent rain showers",
    85: "Slight snow showers",
    86: "Heavy snow showers",
    95: "Thunderstorm",
    96: "Thunderstorm with slight hail",
    99: "Thunderstorm with heavy hail"
};

document.addEventListener("DOMContentLoaded", async () => {
    const todayDate = document.getElementById("today-date").value;
    const homeCurrency = document.getElementById("fetch-home-currency").textContent.trim().toUpperCase();
    const tripCurrency = document.getElementById("fetch-trip-currency").textContent.trim().toUpperCase();

    const cityName = document.getElementById("city-name").value.trim().toLowerCase();
    const infoForAPIs = document.querySelectorAll(".fetch-info");

    let latitude, longitude;
    try {
        const geoRes = await fetch(`https://geocoding-api.open-meteo.com/v1/search?name=${encodeURIComponent(cityName)}&count=1&language=en&format=json`)

        if (!geoRes.ok) {
            weatherResult.textContent = "Unknown city";
            throw new Error(`Error with fetching longtitude and latitude: ${geoRes.statusText}`);
        }

        const geoData = await geoRes.json();

        latitude = geoData.results[0].latitude;
        longitude = geoData.results[0].longitude;
    } catch (e) {
        console.log(`Error with fetching location of the city: ${e}`);
    }

    for (const info of infoForAPIs) {
        // Weather API: //
        const date = info.dataset.date;
        const activityId = info.dataset.activityId;

        const weatherResult = document.getElementById(`weather-result-${activityId}`);
        const conversionResult = document.getElementById(`currency-convertion-${activityId}`);

        try {
            const weatherRes = await fetch(`https://api.open-meteo.com/v1/forecast?latitude=${latitude}&longitude=${longitude}&start_date=${date}&end_date=${date}&daily=temperature_2m_max,temperature_2m_min,weathercode&timezone=auto`);

            if (!weatherRes.ok) {
                weatherResult.textContent = "Unknown weather condition";
                throw new Error(`Error with fetching weather data: ${weatherRes.statusText}`);
            }

            const weatherData = await weatherRes.json();
            const minTemp = weatherData.daily.temperature_2m_min[0];
            const maxTemp = weatherData.daily.temperature_2m_max[0];
            const weatherCode = weatherData.daily.weathercode[0];

            let weatherName = weatherCodeMap[weatherCode];
            if (weatherName.length === 0) {
                weatherName = "Unknown weather condition";
            }

            weatherResult.textContent = `${minTemp}°C - ${maxTemp}°C, ${weatherName}`;

            // Currency exchange API: //
            const rawAmount = info.dataset.amount.replace(',', '.');
            const amount = parseFloat(rawAmount);

            const currencyRes = await fetch(`https://api.frankfurter.dev/v1/${todayDate}?amount=${amount}&from=${tripCurrency}&to=${homeCurrency}`);

            if (!currencyRes.ok) {
                conversionResult.textContent = "Unable to convert currency"
                throw new Error(`Error with converting currency: ${currencyRes.statusText}`);
            }

            const currencyData = await currencyRes.json();
            const convertedAmount = currencyData.rates[homeCurrency];
            conversionResult.textContent = `${convertedAmount.toFixed(2)} ${homeCurrency}`;
        } catch (e) {
            console.log(`Error: ${e}`);
        }
    }
});