const currencyInput = document.getElementById("currencies-search");
const currencyResults = document.getElementById("search-currencies-results");

async function loadCurrencies(query) {
    try {
        const currencyRes = await fetch(`https://api.vatcomply.com/currencies`);

        if (!currencyRes.ok) {
            throw new Error(`Error with fetching JSON with currencies: ${currencyRes.statusText}`);
        }

        const currencyData = await currencyRes.json();

        return Object.entries(currencyData).map(([code, details]) => ({
            code: code,
        })).filter(c => c.code.includes(query)).slice(0, 5);
    } catch (e) {
        console.log(`Error: ${e}`)
    }
}

currencyInput.addEventListener("input", async (e) => {
    let results = await loadCurrencies(e.target.value.trim().toUpperCase());

    currencyResults.innerHTML = "";
    currencyResults.classList.remove("d-none");

    for (const res of results) {
        const listItem = document.createElement("li");
        listItem.textContent = `${res.code}`;

        listItem.classList.add("list-group-item", "list-group-item-action");
        listItem.style.cursor = "pointer";

        listItem.addEventListener("click", () => {
            currencyInput.value = res.code.trim();

            const selectCurrency = document.getElementById("select-currency");
            selectCurrency.value = res.code;

            currencyResults.innerHTML = "";
        });

        currencyResults.appendChild(listItem);
    }
});