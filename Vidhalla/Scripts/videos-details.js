$(document).ready(e => {
    var snackbar = msg => $.mSnackbar(msg);

    $("button").on("mousedown", e => e.preventDefault());

    $("#likeVideoBtn").on("click",
        e => {
            snackbar("You liked this video");
            setTimeout(function () { $.mSnackbar().close(); }, 2800);
        });

    $("#subscribeBtn").on("click",
        e => {
            snackbar("Added to subscriptions");
            setTimeout(function () { $.mSnackbar().close(); }, 2800);
        });
});