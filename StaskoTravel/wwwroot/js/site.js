// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const searchInput = document.getElementById("activitySearch");
const searchResults = document.getElementById("searchResults");

async function getDataFromAPI(query) {
    let title = query.trim().toLowerCase();

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