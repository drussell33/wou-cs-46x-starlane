function getCurrentUserFromSession() {
    let url = window.location.origin + "/api/sessionuser";
    if ($("#myProfileLink").length) {
        return $.ajax({
            url: url,
            method: 'GET',
        })
    }
}

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
    // need to get user.Id instead of their username, sillybilly
    getCurrentUserFromSession().then(function (user) {
        let payload = {follower: user.id, followed: $(button).attr("value-target")};
        sendAjaxRequestWithPayloadAndRoute(payload, url).then(function (response) {
            if (response.success === true) {
                console.log("Followed " + response.follower + " to " + response.followed);
                $(button).text("Following");
                $(button).toggleClass("follow-button").toggleClass("following-button");
            }
            else {
                console.log("Operation failed");
            }
        })
    })
});

/* Unfollow User */
$(document).on("click", '.following-button', function () {
    let button = this;
    let url = window.location.origin + "/api/unfollow";
    getCurrentUserFromSession().then(function (user) {
        let payload = { follower: user.username, followed: $(button).attr("value-target") };
        sendAjaxRequestWithPayloadAndRoute(payload, url).then(function (response) {
            if (response.success === true) {
                console.log("Followed " + response.follower + " to " + response.followed);
                $(button).text("Following");
                $(button).toggleClass("follow-button").toggleClass("following-button");
            }
            else {
                console.log("Operation failed");
            }
        })
    })
    $(button).text("Follow");
    console.log("unfollow");
    $(button).toggleClass("follow-button").toggleClass("following-button").removeClass("btn-danger").addClass("btn-primary");
});

/* Hover Following User */
$(document).on("mouseenter", '.following-button', function () {
    let button = this;
    $(button).text("Unfollow?");
    console.log("unfollow?");
    $(button).addClass("btn-danger").removeClass("btn-primary");
}).on("mouseleave", '.following-button', function () {
    let button = this;
    $(button).text("Following");
    $(button).removeClass("btn-danger").addClass("btn-primary");
});

// For un-following users 
$('#unfollow-button > button').click(function () {
    var followID = this.id.substring(1);      // remove leading 'f'

    $.ajax({
        url: 'following',
        data: { id: followID },
        method: 'POST',
        success: updateFollowees
    });
});

// For following users 
$('#follow-button > button').click(function () {
    var followID = this.id;
    var followName = this.name;

    $.ajax({
        url: 'userpage/follow',
        data: { id: followID, status: followName },
        method: 'POST',
        success: updateFollowees
    });
});

function updateFollowees(data) {

    location.reload(true);
}