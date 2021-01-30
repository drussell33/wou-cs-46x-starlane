// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#statsSection").ready(function() {
    console.log("page loaded site.js");
    var address = "api/stats";
    $.ajax({
        type: "GET",
        dataType: "json",
        url: address,
        success: displayStats,
        error: errorOnAjax
    });
});

function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function displayStats(data) {
    $(".statsTitleSection").empty();
    $(".statsTitleSection").append("<h4>Live Stats at Himalayan Database</h4>");
    $("#statsSection").empty();
    $("#statsSection").append($('<div class="card"><div class="card-body text-center"><p class="card-title largerCardText">Total number of expeditions:</p><h6 class="card-title numberCardText">' + data["numExp"] + "</h6></div></div>"));
    $("#statsSection").append($('<div class="card"><div class="card-body text-center"><p class="card-title largerCardText">Total number of Peaks:</p><h6 class="card-title numberCardText">' + data["numPeaks"] + "</h6></div></div>"));
    $("#statsSection").append($('<div class="card"><div class="card-body text-center"><p class="card-title largerCardText">Total number of Peaks that have yet to be successfully climbed:</p><h6 class="card-title numberCardText">' + data["numUnclimbed"] + "</h6></div></div>"));
}



$("#latestSection").ready(function () {
    var address = "api/latest";
    $.ajax({
        type: "GET",
        dataType: "json",
        url: address,
        success: displayLatest,
        error: errorOnAjax
    });
});

function displayLatest(data) {
    $("#latestSection").empty();
    $("#latestSection").append($('<div class="card"><div class="card-body text-center"><p class="card-title">The Most recent expedition was on the peak:</p><h6 class="card-title numberCardText">' + data["peakName"] + '</h6><p class="card-title">The Expedition Ended as: </p><h6 class="card-title numberCardText">' + data["terminationReason"] + '</h6 > <p class="card-title">And was hosting by the Trekking Agency: </p><h6 class="card-title numberCardText">' + data["trekkingName"] + "</h6></div></div>"));
}

$("#randomPeaks").ready(function () {
    var address = "api/random_peak";
    $.ajax({
        type: "GET",
        dataType: "json",
        url: address,
        success: displayPeak,
        error: errorOnAjax
    });
});

function displayPeak(data) {
    $("#randomPeaks").empty();
    for (let i = 0; i < data.length; ++i) {
        $("#randomPeaks").append($(
            '<div class="card"><div class="card-body text-center"><p class="card-title">Featured Peak Name: </p><h6 class="card-title numberCardText">' +
            data[i]["name"] +
            '</h6><p class="card-title">Its height is: </p><h6 class="card-title numberCardText">' +
            data[i]["height"] +
            '</h6 > <p class="card-title">Allowed to Climb?: </p><h6 class="card-title numberCardText">' +
            data[i]["climbingStatus"] +
            "</h6></div></div>"));
    }
}