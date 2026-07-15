// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener("DOMContentLoaded", () => {
    const bookingTabs = document.querySelectorAll("#bookingTab .nav-link");
    const destinationInput = document.getElementById("destination");
    const searchBtn = destinationInput?.closest("form")?.querySelector("button[type='button']");

    bookingTabs.forEach((tab) => {
        tab.addEventListener("click", (event) => {
            event.preventDefault();
            bookingTabs.forEach((item) => item.classList.remove("active"));
            tab.classList.add("active");
        });
    });

    if (!searchBtn) {
        return;
    }

    searchBtn.addEventListener("click", (event) => {
        event.preventDefault();

        const destination = destinationInput ? destinationInput.value.trim() : "";
        const originalText = searchBtn.innerHTML;

        searchBtn.innerHTML = '<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span> Searching...';
        searchBtn.disabled = true;

        setTimeout(() => {
            searchBtn.innerHTML = originalText;
            searchBtn.disabled = false;

            if (destination) {
                alert(`Searching for top results in ${destination}...`);
                return;
            }

            alert("Please enter a destination to search.");
        }, 1200);
    });
});
