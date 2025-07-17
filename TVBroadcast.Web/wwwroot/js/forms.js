function validateForm() {
    let valid = true;

    const title = document.getElementById("Title").value.trim();
    const genre = document.getElementById("Genre").value.trim();
    const desc = document.getElementById("Description").value.trim();
    const duration = document.getElementById("DurationMinutes").value.trim();
    const status = document.getElementById("ApprovalStatus").value.trim();
    const time = document.getElementById("Time").value.trim();

    document.getElementById("errorTitle").textContent = title === "" ? "Title is required" : "";
    document.getElementById("errorGenre").textContent = genre === "" ? "Genre is required" : "";
    document.getElementById("errorDesc").textContent = desc === "" ? "Description is required" : "";
    document.getElementById("errorDuration").textContent = duration === "" || parseInt(duration) <= 0 ? "Duration must be positive" : "";
    document.getElementById("errorStatus").textContent = status === "" ? "Status is required" : "";
    document.getElementById("errorTime").textContent = time === "" ? "Time is required" : "";

    if (!title || !genre || !desc || !status || !time || parseInt(duration) <= 0) {
        valid = false;
    }

    return valid;
}
//-----------Login form js------------

function validateLoginForm() {
    let valid = true;

    const email = document.getElementById("Email").value.trim();
    const password = document.getElementById("Password").value.trim();

    document.getElementById("errorEmail").textContent = email === "" ? "Email is required" : "";
    document.getElementById("errorPassword").textContent = password.length < 6 ? "Password must be at least 6 characters" : "";

    if (!email || password.length < 6) {
        valid = false;
    }

    return valid;
}

