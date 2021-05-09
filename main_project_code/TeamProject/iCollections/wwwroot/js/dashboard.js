// Dashboard specific functionality

$(window).on('load', function () {
    var len = $(".feed-object").length;
    if (len > 0) {
        var lastFeedObject = $(".feed-object")[len - 1];
    }
});