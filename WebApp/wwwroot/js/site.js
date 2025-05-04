console.log("site.js loaded")

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

window.editProjectOpen = function (projectId) {
    fetch('/Projects/EditProject?projectId=' + projectId)
        .then(response => response.text())
        .then(html => {
            document.getElementById("edit-project-content").innerHTML = html;
            document.getElementById('edit-project-bg').style.display = 'block';
            document.getElementById('edit-project-card').style.display = 'block';
        });
}

function editProjectClose() {
    document.getElementById('edit-project-bg').style.display = 'none';
    document.getElementById('edit-project-card').style.display = 'none';
    document.getElementById('edit-project-content').innerHTML = "";
}

// Submits

async function submitEditProfile() {
    const form = document.getElementById('update-profile');
    const formData = new FormData(form);

    const response = await fetch('/Account/UpdateProfile', {
        method: 'POST',
        body: formData
    });
    if (response.ok) {
        editProfileClose();
        showSuccessToast("Profile updated")
    }
}

function submitEditProject(e)
{
    e.preventDefault();

    const form = e.target;
    const formData = new FormData(form)

    fetch(form.action, {
        method: 'POST',
        body: formData
    })
        .then(res => {
            const contentType = res.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                return res.json();
            } else {
                return res.text();
            }
        })
        .then(data => {
            if (data.success) {
                editProjectClose();

                fetch('/Projects/LoadProjectsPartial')
                    .then(res => res.text())
                    .then(html => {
                        document.getElementById('projects-container').innerHTML = html;

                        showSuccessToast("Project updated")
                    });
            } else {
                document.getElementById('edit-project-content').innerHTML = data;
            }
        });
}

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
        reloadProjectsList();
        showSuccessMessage("Project Created")
    } else {
        const errorText = await response.text();
        showErrorMessage("Project not created" + errorText)
    }
}

function completeProject(projectId) {
    fetch('/Projects/completeProject', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        },
        body: JSON.stringify({ id: projectId })
    })
        .then(response => {
            if (response.ok) {
                showSuccessMessage("Project Completed")
                setTimeout(() => {
                    location.reload();

                }, 3000);
            } else {
                alert("Failed to completed project");
            }
        });
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

function showSuccessToast(message) {
    const container = document.getElementById('alert-container')

    const toast = document.createElement('div');
    toast.className = 'alert-success';
    toast.textContent = message;

    container.appendChild(toast);

    setTimeout(() => {
        toast.remove();
    }, 3000)
}

function showErrorToast(message) {
    const container = document.getElementById('alert-container')

    const toast = document.createElement('div');
    toast.className = 'alert-error';
    toast.textContent = message;

    container.appendChild(toast);

    setTimeout(() => {
        toast.remove();
    }, 3000)
}

// Dropdown

function toggleDropdownUser() {
    var dropdown = document.getElementById('dropdown-user');
    const avatarImg = document.querySelector('.avatar-img')

    dropdown.classList.toggle('active');
    avatarImg.classList.toggle('active');
}

function toggleDropdownProject(clickedElement) {
    document.querySelectorAll('.container-projects-menu').forEach(el => {
        if (el !== clickedElement) {
            el.classList.remove('show'),
            el.classList.remove('active');
        }
    });

    clickedElement.classList.toggle('show')
    clickedElement.classList.toggle('active')
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

// Filters

function filterCompletedProjects(type) {
    const cards = document.querySelectorAll('.card-projects');

    cards.forEach(card => {
        const status = card.getAttribute('data-status');

        if (type === 'all' || type === status) {
            card.style.display = "block";
        } else {
            card.style.display = "none";
        }
    });
} 

document.addEventListener('DOMContentLoaded', () => {
    filterCompletedProjects('all');
})

// Reloads

function reloadProjectsList() {
    fetch('/Projects/LoadProjectsPartial')
        .then(response => response.text())
        .then(html => { document.getElementById('projects-container').innerHTML = html; });
}

// Logout

function logout() {
    window.location.href = '/Account/Logout';
}



