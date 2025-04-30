
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
    document.getElementById('edit-profile-bg').style.display = 'block';
    document.getElementById('edit-profile-card').style.display = 'block';
}

function editProfileClose() {
    document.getElementById('edit-profile-bg').style.display = 'none';
    document.getElementById('edit-profile-card').style.display = 'none';
}

async function submitEditProfile() {
    const form = document.getElementById('update-profile');
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
    const bg = document.getElementById('add-project-bg');
    const card = document.getElementById('add-project-card');

    if (!bg || !card) {
        console.error('Element not found.')
        return;
    }
    bg.style.display = 'block';
    card.style.display = 'block';
}

function createProjectClose() {
    document.getElementById('add-project-bg').style.display = 'none';
    document.getElementById('add-project-card').style.display = 'none';
}

document.addEventListener('DOMContentLoaded', function () {
    const closeBtn = document.querySelector('.close-window');
    if (closeBtn) {
        closeBtn.addEventListener('click', createProjectClose);
    }
    const bg = document.getElementById('add-project-bg');
    if (bg) {
        bg.addEventListener('click', createProjectClose)
    }
})

async function submitCreateProject() {

    console.log("submitCreateProject called");

    const form = document.getElementById('create-project');
    const formData = new FormData(form);

    const response = await fetch('/Home/CreateProject', {
        method: 'POST',
        body: formData
    });
    if (response.ok) {
        createProjectClose();
        showSuccessMessage("Project Created")
    } else {
        const errorText = await response.text();
        showErrorMessage("Project not created" + errorText)
    }
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



