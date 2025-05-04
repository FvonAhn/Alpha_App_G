// Tooltip

document.addEventListener('DOMContentLoaded', function () {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
    console.log("Antal tooltips hittade:", tooltipTriggerList.length);
    tooltipTriggerList.forEach(tooltipTriggerEl => {
        new bootstrap.Tooltip(tooltipTriggerEl)
    })
});

// TinyMCE

function initializeTinyMCE() {
    if (tinymce.get("Description")) {
        tinymce.get("Description").remove();
    }

    tinymce.init({
        selector: '#Description',
        height: 150,
        menubar: false,
        plugins: 'link lists code',
        toolbar: 'undo redo | bold italic underline | bullist numlist | link | code',
        branding: false
    });
}


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

    initializeTinyMCE();
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

            initializeTinyMCE();
        });
}

function editProjectClose() {
    document.getElementById('edit-project-bg').style.display = 'none';
    document.getElementById('edit-project-card').style.display = 'none';
    document.getElementById('edit-project-content').innerHTML = "";
}

// Validation

document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("auth-register");

    if (form) {
        form.addEventListener("submit", function (e) {
            let isValid = true;

            const fullName = document.getElementById("FullName");
            const email = document.getElementById("Email");
            const password = document.getElementById("Password");
            const confirmPassword = document.getElementById("ConfirmPassword");
            const terms = document.getElementById("TermsAndConditions");

            [fullName, email, password, confirmPassword].forEach(input => {
                input.placeholder = input.getAttribute("data-original-placeholder") || input.placeholder;
                input.classList.remove("input-error");
            });

            if (fullName.value.trim() === "") {
                fullName.placeholder = "Full name is required";
                fullName.classList.add("input-error");
                isValid = false;
            }

            if (email.value.trim() === "") {
                email.placeholder = "Email is required";
                email.classList.add("input-error");
                isValid = false;
            } else if (!email.value.includes("@")) {
                email.value = "";
                email.placeholder = "Enter a valid email";
                email.classList.add("input-error");
                isValid = false;
            }

            if (password.value.length < 6) {
                password.value = "";
                password.placeholder = "Min 6 characters";
                password.classList.add("input-error");
                isValid = false;
            }

            if (confirmPassword.value !== password.value) {
                confirmPassword.value = "";
                confirmPassword.placeholder = "Passwords do not match";
                confirmPassword.classList.add("input-error");
                isValid = false;
            }

            if (isValid && !terms.checked) {
                alert("You must agree to the terms and conditions.");
                isValid = false;
            }

            if (!isValid) {
                e.preventDefault();
            }
        });

        ["FullName", "Email", "Password", "ConfirmPassword"].forEach(id => {
            const input = document.getElementById(id);
            if (input) {
                input.setAttribute("data-original-placeholder", input.placeholder);
            }
        });
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const loginForm = document.getElementById("login");

    if (loginForm) {
        loginForm.addEventListener("submit", function (e) {
            let isValid = true;

            const email = document.getElementById("Email");
            const password = document.getElementById("Password");

            [email, password].forEach(input => {
                input.placeholder = input.getAttribute("data-original-placeholder") || input.placeholder;
                input.classList.remove("input-error");
            });

            if (email.value.trim() === "") {
                email.placeholder = "Email is required";
                email.classList.add("input-error");
                isValid = false;
            } else if (!email.value.includes("@")) {
                email.value = "";
                email.placeholder = "Enter a valid email";
                email.classList.add("input-error");
                isValid = false;
            }

            if (password.value.trim() === "") {
                password.placeholder = "Password is required";
                password.classList.add("input-error");
                isValid = false;
            }

            if (!isValid) {
                e.preventDefault();
            }
        });

        ["Email", "Password"].forEach(id => {
            const input = document.getElementById(id);
            if (input) {
                input.setAttribute("data-original-placeholder", input.placeholder);
            }
        });
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("edit-profile-form");

    if (form) {
        form.addEventListener("submit", function (e) {
            const fullName = document.getElementById("FullName");
            const email = document.getElementById("Email");
            const newPassword = document.getElementById("NewPassword");
            const confirmPassword = document.getElementById("ConfirmNewPassword");

            let isValid = true;

            [fullName, email, newPassword, confirmPassword].forEach(input => {
                if (input) {
                    input.classList.remove("input-error");
                    input.placeholder = input.getAttribute("data-original-placeholder") || input.placeholder;
                }
            });

            if (fullName && fullName.value.trim() === "") {
                fullName.placeholder = "Full name is required";
                fullName.classList.add("input-error");
                isValid = false;
            }

            if (email && email.value.trim() !== "") {
                if (!email.value.includes("@") || !email.value.includes(".")) {
                    email.value = "";
                    email.placeholder = "Invalid email";
                    email.classList.add("input-error");
                    isValid = false;
                }
            }

            if (newPassword && newPassword.value.length > 0 && newPassword.value.length < 6) {
                newPassword.value = "";
                newPassword.placeholder = "Min 6 characters";
                newPassword.classList.add("input-error");
                isValid = false;
            }

            if (newPassword && newPassword.value.length > 0) {
                if (confirmPassword.value !== newPassword.value) {
                    confirmPassword.value = "";
                    confirmPassword.placeholder = "Passwords do not match";
                    confirmPassword.classList.add("input-error");
                    isValid = false;
                }
            }

            if (!isValid) e.preventDefault();
        });

        ["FullName", "Email", "NewPassword", "ConfirmNewPassword"].forEach(id => {
            const input = document.getElementById(id);
            if (input) {
                input.setAttribute("data-original-placeholder", input.placeholder);
            }
        });
    }
});


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
    tinymce.triggerSave();

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
    tinymce.triggerSave();

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

                }, 1500);
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
}

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



