// For un-following users 
$('#unfollow-button > button').click(function () {
    var followID = this.id.substring(1);      // remove leading 'f'

    $.ajax({
        url: 'following',
        data: { id: followID },
        // add token in header?
        method: 'POST',
        success: updateFollowees
    });
});

function updateFollowees(data) {

    location.reload(true);
}

$(document).on("click", '.following-button', function () {
    let button = this;
    $(button).text("Follow");
    console.log("unfollow");
    $(button).toggleClass("follow-button").toggleClass("following-button").removeClass("btn-danger").addClass("btn-primary");
});

$(document).on("click", '.follow-button', function () {
    let button = this;
    $(button).text("Following");
    console.log("follow");
    $(button).toggleClass("follow-button").toggleClass("following-button");
});

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