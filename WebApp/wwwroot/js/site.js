
// Tooltip

document.addEventListener('DOMContentLoaded', function () {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
    console.log("Antal tooltips hittade:", tooltipTriggerList.length);
    tooltipTriggerList.forEach(tooltipTriggerEl => {
        new bootstrap.Tooltip(tooltipTriggerEl)
    })
});

// Modals

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
    const formData = new FormData(form);

    const response = await fetch('/Account/UpdateProfile', {
        method: 'POST',
        body: formData
    });
    if (response.ok) {
        editProfileClose();
        showSuccessMessage("Profile updated")
    }
}

function createProjectOpen() {
    document.getElementById('create-project-modal').style.display = 'block';
}

function createProjectClose() {
    document.getElementById('create-project-modal').style.display = 'none';
}

// Alerts

function showSuccessMessage(message) {
    const msg = document.createElement('div');
    msg.className = 'alert-success';
    msg.innerText = message;

    document.body.appendChild(msg);

    setTimeout(() => {
        msg.remove();
    }, 3000);
}

function showErrorMessage(message) {
    const msg = document.createElement('div');
    msg.className = 'alert-error';
    msg.innerText = message;

    document.body.appendChild(msg);

    setTimeout(() => {
        msg.remove();
    }, 3000);
}

// Dropdown

function toggleDropdownUser() {
    var dropdown = document.getElementById('dropdown-user');
    dropdown.classList.toggle('active');
}

// Uploads

function avatarUpload() {
    document.getElementById('avatar-upload').click();
}

function previewAvatar(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            document.getElementById('avatar-preview').src = e.target.result;
        }
        reader.readAsDataURL(input.files[0]);
    }
} // denna är avskriven från chatGpt. Jag kopierar aldrig rakt av.

// Logout

function logout() {
    window.location.href = '/Account/Logout';
}



