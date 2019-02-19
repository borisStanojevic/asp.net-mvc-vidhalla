$(document).ready(e => {
    //$("#videosSearchSection").hide();
    $(".ghostBtn").on("click", () => {
        $(".jumbotron").hide();
        $("#videosSection").focus();
    });

});