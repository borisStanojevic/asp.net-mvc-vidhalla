$(document).ready(e => {

    var videoId = window.location.href.substr(window.location.href.lastIndexOf("/") + 1).trim();

    var snackbar = function (msg) {
        $.mSnackbar(msg);
        setTimeout(function () { $.mSnackbar().close(); }, 2700);
    }

    $("button").on("mousedown", e => e.preventDefault());

    $("#likeVideoBtn").on("click", () => {
        snackbar("You liked this video");
    });


    $("#subscribeBtn").click(function (e) {
        var $this = $(this);
        var account = $this.data("account");
        var action = $this.attr("data-action");
        if (action === "Login") {
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
                    snackbar(snackbarMsg);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        e.preventDefault();
    });

    $("#postCommentBtn").click(function (e) {
        e.preventDefault();

        var action = $(this).attr("data-action");
        if (action === "Login") {
            window.location.href = "/accounts/login";
        }

        var content = $("#myCommentInput").val().trim();

        $.ajax("/comments/create",
            {
                type: "POST",
                data: { content: content, videoId: videoId },

                success: function (data) {
                    if (data.errorMessage) {
                        alert(data.errorMessage);
                        return;
                    };
                    $("#myCommentInput").val("");
                    var $commentLi = createCommentListItem(data);
                    $("#commentsList").prepend($commentLi);
                },
                error: function (error) {
                    alert(error);
                }
            });
    });

    $("#sortOrderSelect").change(function () {
        var sortOrder = $(this).val();

        $.ajax("/comments",
            {
                type: "POST",
                dataType: "json",
                data: { videoId: videoId, sortOrder: sortOrder },

                success: function (data) {
                    var $commentsList = $("#commentsList");
                    $commentsList.empty();
                    data.forEach(function (comment) {
                        var $commentLi = createCommentListItem(comment);
                        $commentsList.append($commentLi);
                    });
                },
                error: function (error) {
                    console.log("Error");
                }
            });
    });


    $("#commentsList").on("click",
        ".deleteCommentBtn",
        function (e) {
            e.preventDefault();


            var $this = $(this);
            var action = $this.attr("data-action");
            if (action === "Login") {
                window.location.href = "/accounts/login";
            }
            var commentId = parseInt($this.data("commentid"));

            $.ajax("/comments/delete",
                {
                    type: "POST",
                    dataType: "json",
                    data: { id: commentId },

                    success: function (data) {
                        if (data.errorMessage) {
                            alert(data.errorMessage);
                            return;
                        };
                        var listItemToRemove = $this.closest("li");
                        listItemToRemove.remove();
                        snackbar(data.message);
                    },
                    error: function () {
                        snackbar("Error deleting comment");
                    }

                });
        });

    $("#likeVideoBtn").click(function (e) {
        e.preventDefault();

        $.ajax("/video-votes/create",
            {
                type: "POST",
                dataType: "json",
                data: { videoId: videoId, type: "LIKE" },

                success: function (data) {

                },
                error: function () {
                    snackbar("Error liking video");
                }
            });
    });

    $("#dislikeVideoBtn").click(function (e) {
        e.preventDefault();

        $.ajax("/video-votes/create",
            {
                type: "POST",
                dataType: "json",
                data: { videoId: videoId, type: "DISLIKE" },

                success: function (data) {

                },
                error: function () {
                    snackbar("Error liking video");
                }
            });
    });

});

function createCommentListItem(comment) {

    return `<li>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <img src='/accounts/loadprofilepicture?profilePicture=${comment.CommenterProfilePicture}' class='smallThumbnail'>
                        <a href='accounts/details/${comment.CommenterUsername}'>${comment.CommenterUsername}</a>
                        <span> posted on ${comment.DatePosted}</span>
                        <button type="button" class="btn btn-danger btn-sm deleteCommentBtn" data-commentid=${comment.Id}>
                        <span class="glyphicon glyphicon-trash"></span></button>
                    </div>

                    <div class="panel-body">
                        ${comment.Content}
                        <br>
                        <hr>
                    </div>
                </div>
            </li>`;
};