const searchInput = document.getElementById("activity-search");
const searchResults = document.getElementById("search-results");

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