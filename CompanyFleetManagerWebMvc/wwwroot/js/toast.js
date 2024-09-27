const TOAST_DURATION_MS = 2000;

function showToast(message, type = "") {
    let toast;

    switch (type) {
        case "success":
            toast = document.querySelector(".toast_success");
            break;
        case "warning":
            toast = document.querySelector(".toast_warning");
            break;
        case "error":
            toast = document.querySelector(".toast_error");
            break;
        default:
            toast = document.querySelector(".toast");
            break;
    }

    toast.classList.add("show");
    toast.innerHTML = message;
    setTimeout(function () {
        toast.classList.remove("show");
    }, TOAST_DURATION_MS);
}

function showToastThenSubmit(message, formId, type="") {
    showToast(message, type);

    setTimeout(function () {
        document.getElementById(formId).submit();
    }, TOAST_DURATION_MS);
}