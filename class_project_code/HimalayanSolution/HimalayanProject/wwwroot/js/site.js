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
    $("#statsSection").empty();

    $("#statsSection").append($('<p class="titleText" >Total number of expeditions: ' + data["numExp"] + "</p>"));
    $("#statsSection").append($('<p class="titleText" >Total number of Peaks: ' + data["numPeaks"] + "</p>"));
    $("#statsSection").append($('<p class="titleText" >Total number of Peaks that have yet to be successfully climbed:' + data["numUnclimbed"] + "</p>"));
      

}