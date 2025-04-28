
// Tooltip

document.addEventListener('DOMContentLoaded', function () {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
    console.log("Antal tooltips hittade:", tooltipTriggerList.length);
    tooltipTriggerList.forEach(tooltipTriggerEl => {
        new bootstrap.Tooltip(tooltipTriggerEl)
    })
});

// Modal

function editProfileOpen() {
    document.querySelector('.modal-background').style.display = 'block';
    document.querySelector('.modal-add-card').style.display = 'block';
}

function editProfileClose() {
    document.querySelector('.modal-background').style.display = 'none';
    document.querySelector('.modal-add-card').style.display = 'none';
}

async function submitEditProfile() {
    const form = document.querySelector('.modal-form');
    const formData = document.querySelector(form);

    const response = await fetch('/Account/Edit', {
        method: 'POST',
        body: formData
    });
    if (response.ok) {
        editProfileClose();
        showSuccesMessage("Profile updated")
    }
}

function editSuccessMessage(message) {
    const msg = documet.createElement('div');
    msg.className = 'edit-success';
    msg.innerText = message;

    document.body.appendChild(msg);

    setTimeout(() => {
        msg.remove();
    }, 2500);
}

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



