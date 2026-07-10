// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener("DOMContentLoaded", () => {
    const bookingTabs = document.querySelectorAll("#bookingTab .nav-link");
    const searchBtn = document.querySelector(".btn-primary.w-100");

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

        const destinationInput = document.getElementById("destination");
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
