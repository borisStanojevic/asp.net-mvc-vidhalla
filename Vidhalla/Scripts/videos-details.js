$(document).ready(e => {

    $("button").on("mousedown", e => e.preventDefault());

    $("#likeVideoBtn").on("click", () => {
        $.mSnackbar("You liked this video");
        setTimeout(function () { $.mSnackbar().close(); }, 3000);
    });


    $("#subscribeBtn").click(function (e) {

        var $this = $(this);
        var account = $this.data("account");
        var action = $this.attr("data-action").toLowerCase();
        if (action.valueOf() === "Login".valueOf()) {
            window.location.href = "/accounts/login";
        }

        $.ajax(`/accounts/${action}`,
            {
                type: "POST",
                data: { "id": account },

                success: function (data) {
                    var nextAction = new String(data.nextAction);
                    $this.text(nextAction);
                    $this.attr("data-action", nextAction);

                    var $subscriptionDiv = $("#subscriptionDiv").find("span");
                    if (nextAction.valueOf() === "Unsubscribe".valueOf())
                        $subscriptionDiv.text(parseInt($subscriptionDiv.text()) + 1);
                    else
                        $subscriptionDiv.text(parseInt($subscriptionDiv.text()) - 1);

                    var snackbarMsg = nextAction.valueOf() === "Unsubscribe".valueOf() ? "Added to subscriptions" :
                        "Removed from subscriptions";
                    $.mSnackbar(snackbarMsg);
                    setTimeout(function () { $.mSnackbar().close(); }, 2500);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        e.preventDefault();
    });

});