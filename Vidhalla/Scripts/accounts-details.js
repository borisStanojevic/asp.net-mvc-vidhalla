$(document).ready(() => {

    $("button").on("mousedown", e => e.preventDefault());

    $("#showUploadVideoBtn").on("click", function (e) {
        $("#uploadVideoDialog").modal();
        e.preventDefault();
    });

});
