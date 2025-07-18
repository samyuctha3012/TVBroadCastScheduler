
function validateForm() {
    let valid = true;

    // Basic field validation
    const title = document.getElementById("Title").value.trim();
    const genre = document.getElementById("Genre").value.trim();
    const desc = document.getElementById("Description").value.trim();
    const status = document.getElementById("ApprovalStatus").value.trim();

    const start = document.getElementById("StartTime").value;
    const end = document.getElementById("EndTime").value;
    const durationInput = document.getElementById("DurationMinutes");

    // Error labels
    const errorTitle = document.getElementById("errorTitle");
    const errorGenre = document.getElementById("errorGenre");
    const errorDesc = document.getElementById("errorDesc");
    const errorStatus = document.getElementById("errorStatus");
    const errorStart = document.getElementById("errorStartTime");
    const errorEnd = document.getElementById("errorEndTime");
    const errorDuration = document.getElementById("errorDuration");

    // Clear all error messages
    errorTitle.textContent = "";
    errorGenre.textContent = "";
    errorDesc.textContent = "";
    errorStatus.textContent = "";
    errorStart.textContent = "";
    errorEnd.textContent = "";
    errorDuration.textContent = "";

    // Validate required text fields
    if (title === "") {
    errorTitle.textContent = "Title is required";
    valid = false;
    }
    if (genre === "") {
    errorGenre.textContent = "Genre is required";
    valid = false;
    }
    if (desc === "") {
    errorDesc.textContent = "Description is required";
    valid = false;
    }
    if (status === "") {
    errorStatus.textContent = "Status is required";
    valid = false;
    }

    // Validate and calculate duration
    if (!start) {
    errorStart.textContent = "Start time is required";
    valid = false;
    }
    if (!end) {
    errorEnd.textContent = "End time is required";
    valid = false;
    }

    if (start && end) {
    const startDate = new Date(`1970-01-01T${start}`);
    const endDate = new Date(`1970-01-01T${end}`);

    if (endDate <= startDate) {
    errorEnd.textContent = "End time must be after start time.";
    valid = false;
    } else {
    const duration = (endDate - startDate) / (1000 * 60); // minutes
    durationInput.value = duration;

    if (duration <= 0) {
    errorDuration.textContent = "Duration must be greater than zero.";
    valid = false;
    }
    }
    }

    return valid;
}


//-----------Login form js------------

function validateLoginForm() {
    let email = document.getElementById("Email").value.trim();
    let password = document.getElementById("Password").value.trim();
    let isValid = true;

    if (email === "") {
        document.getElementById("errorEmail").innerText = "Email is required.";
        isValid = false;
    } else {
        document.getElementById("errorEmail").innerText = "";
    }

    if (password === "") {
        document.getElementById("errorPassword").innerText = "Password is required.";
        isValid = false;
    } else {
        document.getElementById("errorPassword").innerText = "";
    }

    return isValid;
}


