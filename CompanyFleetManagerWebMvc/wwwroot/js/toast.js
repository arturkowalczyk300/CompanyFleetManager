const TOAST_DURATION_MS = 2000;

function showToast(message) {
    var toast = document.getElementById("toast");
    toast.className = "show";
    toast.innerHTML = message;
    setTimeout(function () {
        toast.className = toast.className.replace("show", "");
    }, TOAST_DURATION_MS);
}

function showToastThenSubmit(message, formId) {
    showToast(message);

    setTimeout(function () {
        document.getElementById(formId).submit();
    }, TOAST_DURATION_MS);
}