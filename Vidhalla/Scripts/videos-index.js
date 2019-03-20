$(document).ready(e => {

    $(".ghostBtn").on("click",
        () => {
            $(".jumbotron").hide();
            $("#videosSection").focus();
        });

    $("#sortOrderSelect").on("change", function () {
        var sortOrder = $(this).val();
        window.location.href = `/videos?sortOrder=${sortOrder}`;
    });


});