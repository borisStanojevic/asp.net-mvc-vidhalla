$(document).ready(() => {
  $("#showUploadVideoBtn").on("click", function(e) {
    $("#uploadVideoDialog").modal();
    e.preventDefault();
  });
});
