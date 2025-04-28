
// Tooltip

document.addEventListener('DOMContentLoaded', function () {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
    console.log("Antal tooltips hittade:", tooltipTriggerList.length);
    tooltipTriggerList.forEach(tooltipTriggerEl => {
        new bootstrap.Tooltip(tooltipTriggerEl)
    })
});

// Modal

// Dropdown

function toggleDropdownUser() {
    var dropdown = document.getElementById('dropdown-user');
    dropdown.classList.toggle('active');
}

// Theme

// Logout

function logout() {
    window.location.href = '/Account/Logout';
}



