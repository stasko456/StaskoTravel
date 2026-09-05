// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// SEARCH INPUT LOGIC //
const searchInput = document.getElementById("activitySearch");
const searchResults = document.getElementById("searchResults");

async function getDataFromAPI(query) {
    let title = query.trim();

    if (title.length < 2) {
        return [];
    }

    try {
        const response = await fetch(`/Activity/Search?title=${encodeURIComponent(title)}`)

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        return await response.json();
    } catch (e) {
        console.log(`Error loading results: ${error}`);
        return [];
    }
}

searchInput.addEventListener("input", async (e) => {
    let results = await getDataFromAPI(e.target.value);

    searchResults.innerHTML = "";
    searchResults.classList.remove("d-none");

    results.forEach((result) => {
        const listItem = document.createElement("li");

        listItem.textContent = result.title;
        listItem.classList.add("list-group-item", "list-group-item-action");
        listItem.style.cursor = "pointer";

        console.log(result);
        listItem.addEventListener("click", () => {
            searchInput.value = result.title;

            const selectedActivityId = document.getElementById("selectedActivityId");
            selectedActivityId.value = result.id;

            searchResults.innerHTML = "";
            searchResults.classList.add("d-none");
        })

        searchResults.appendChild(listItem);
    });
});

// WEATHER API //
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
    const cityName = document.getElementById("cityName").value.trim().toLowerCase();
    const infoForWeather = document.querySelectorAll(".fetch-weather");

    for (const info of infoForWeather) {
        const date = info.dataset.date;
        const activityId = info.dataset.activityId;

        const weatherResult = document.getElementById(`weather-result-${activityId}`);

        try {
            const geoRes = await fetch(`https://geocoding-api.open-meteo.com/v1/search?name=${cityName}&count=1&language=en&format=json`)

            if (!geoRes.ok) {
                weatherResult.textContent = "Unknown city";
                throw new Error(`Error with fetching longtitude and latitude: ${geoRes.statusText}`);
            }

            const geoData = await geoRes.json();
            const latitude = geoData.results[0].latitude;
            const longitude = geoData.results[0].longitude;

            const weatherRes = await fetch(`https://api.open-meteo.com/v1/forecast?latitude=${latitude}&longitude=${longitude}&start_date=${date}&end_date=${date}&daily=temperature_2m_max,temperature_2m_min,weathercode&timezone=auto`);

            if (!weatherRes.ok) {
                weatherResult.textContent = "Unknown weather condition";
                throw new Error(`Error with fetchign weather data: ${weatherRes.statusText}`);
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
        } catch (e) {
            console.log(`Error: ${e}`);
        }
    }
});

// CURRENCY EXCHNAGE API //
document.addEventListener("DOMContentLoaded", async () => {
    const todayDate = document.getElementById("today-date").value;
    const homeCurrency = document.querySelector(".fetch-home-currency").textContent.trim();
    const tripCurrency = document.querySelector(".fetch-trip-currency").textContent.trim();
    const estimatedCosts = document.querySelectorAll(".estimated-cost");

    for (const cost of estimatedCosts) {
        const rawAmount = cost.textContent.trim().replace(',', '.');
        const amount = parseFloat(rawAmount);

        const activityId = cost.dataset.activityId;
        const conversionResult = document.getElementById(`currency-convertion-${activityId}`);
        try {
            const currencyRes = await fetch(`https://api.frankfurter.dev/v1/${todayDate}?amount=${amount}&from=${tripCurrency}&to=${homeCurrency}`);

            if (!currencyRes.ok) {
                conversionResult.textContent = "Unable to convert currency."
                throw new Error(`Error with converting currency: ${currencyRes.statusText}`);
            }

            const currencyData = await currencyRes.json();
            const convertedAmount = currencyData.rates[homeCurrency];
            conversionResult.textContent = `${convertedAmount.toFixed(2)} ${homeCurrency}`
        } catch (e) {
            console.log(`Error: ${e}`);
        }
    } 
});