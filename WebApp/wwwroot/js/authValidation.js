<script>
    document.addEventListener("DOMContentLoaded", function () {
        const form = document.getElementById("auth-register");
    
        form.addEventListener("submit", function (e) {
            let isValid = true;

            document.querySelectorAll(".error-message").forEach(el => el.textContent = "");

            const fullName = document.getElementById("FullName");
            if (!fullName.value.trim())
            {
                showError("FullName", "Full name is required");
                isValid = false;
            }

            const email = document.getElementById("Email");
            if (!email.value.trim() || !email.value.includes("@"))
            {
                showError("Email", "Must enter a valid emailadress");
                isValid = false;
            }

            const password = document.getElementById("Password");
            if (!password.value || password.value.length < 6) 
            {
                showError("Password", "Password must contain at least 6 characters");
                isValid = false;
            }

            const confirmPassword = document.getElementById("ConfirmPassword");
            if (confirmPassword.value !== password.value)
            {
                showError("ConfirmPassword", "Passwords do not match");
                isValid = false;
            }

            const terms = document.getElementById("TermsAndConditions");
            if (!terms.checked)
            {
                showError("TermsAndConditions", "You must agree to the terms");
                isValid = false;
            }

            if (!isValid) {
                e.PreventDefault();
            }

            function showError(fieldName, message) {
                const errorSpan = document.querySelector(`[data-valmsg-for="${fieldName}"]`)
                if (errorSpan) {
                    errorSpan.textContent = message;
                }
            }
        });
    });
</script>