function showToast(message) {
    var toast = document.getElementById("toast");
    toast.className = "show";
    toast.innerHTML = message;
    setTimeout(function () {
        toast.className = toast.className.replace("show", "");
    }, 3000);
}