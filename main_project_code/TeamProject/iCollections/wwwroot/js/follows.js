function sendAjaxRequestWithPayloadAndRoute(payload, route) {
    return $.ajax({
        url: route,
        method: 'POST',
        data: payload,
    })
}

/* Follow User */
$(document).on("click", '.follow-button', function () {
    let button = this;
    let url = window.location.origin + "/api/follow";
    let payload = {follower: $(button).attr("value-user"), followed: $(button).attr("value-target")};
    sendAjaxRequestWithPayloadAndRoute(payload, url).then(function (response) {
        if (response.success === true) {
            console.log("Followed " + response.follower + " to " + response.followed);
            $(button).text("Following");
            $(button).toggleClass("follow-button").toggleClass("following-button");
        }
        else {
            console.log("Operation failed " + response.message);
        }
    })
});

/* Unfollow User */
$(document).on("click", '.following-button', function () {
    let button = this;
    let url = window.location.origin + "/api/unfollow";
    let payload = { follower: $(button).attr("value-user"), followed: $(button).attr("value-target") };
    sendAjaxRequestWithPayloadAndRoute(payload, url).then(function (response) {
        if (response.success === true) {
            console.log("Unfollowed " + response.follower + " from " + response.followed);
            $(button).text("Follow");
            $(button).toggleClass("follow-button").toggleClass("following-button").removeClass("btn-danger").addClass("btn-primary");
        }
        else {
            console.log("Operation failed " + response.message);
        }
    })
});

/* Hover Following User */
$(document).on("mouseenter", '.following-button', function () {
    let button = this;
    $(button).text("Unfollow?");
    $(button).addClass("btn-danger").removeClass("btn-primary");
}).on("mouseleave", '.following-button', function () {
    let button = this;
    $(button).text("Following");
    $(button).removeClass("btn-danger").addClass("btn-primary");
});
