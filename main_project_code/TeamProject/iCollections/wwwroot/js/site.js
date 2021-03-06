﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//import { Button } from "bootstrap";        these throw errror and prevent other js from running: Uncaught SyntaxError: import declarations may only appear at top level of a module
//import { data } from "jquery";

//    SHOW UPLOADED IMAGE
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imageResult')
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
        $("#customNameError").hide();
        $("#uploadPhotoError").hide();
    }
    else {
        alert("Please enter an image.")
    }
}

$('#upload').on('change', function () {
    readURL(input);
});


//    SHOW UPLOADED IMAGE NAME

var input = document.getElementById('upload');
var infoArea = document.getElementById('upload-label');

// check if the optional input is correct format 
// and if photo is uploaded
$("#photoUpload").submit(function (event) {
    var optionalName = $("#customName").val();
    if (optionalName !== "") {
        // if bunch of whitespace...
        if (!optionalName.replace(/\s/g, '').length) {
            $("#customNameError").text("names must have letters and/or numbers.").show();
            event.preventDefault();
        }
        else {
            var fileExt = $("#upload")[0].files[0].name.split(".").pop();
            var fullName = optionalName + "." + fileExt;
            $("#customName")[0].value = fullName;
        }
    }

    var fileUpload = $("#upload")[0];
    if (fileUpload.files.length === 0) {
        $("#uploadPhotoError").text("please upload an image.").show();
        event.preventDefault();
    }

});
/*
// when a photo is clicked in view photos, highlight it
$('.pic-thumbnail').click(function (e) {
    if (e.ctrlKey) { }
    else {
        $('.selected-thumbnail').removeClass('selected-thumbnail');
        $(this).addClass('selected-thumbnail');
    }
});

// Set the width of the side navigation to 250px 
function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
}

// Set the width of the side navigation to 0
function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    $('.selected-thumbnail').removeClass('selected-thumbnail');
}

function isValidPhotoName(proposed) {
    if (proposed == null || proposed == "") {
        // User cancelled the prompt -> ignore
        return false;
    }

    if (!proposed.replace(/\s/g, '').length) {
        // User has either all whitespace -> tell user
        alert("Enter a non-empty name");
        return false;
    }

    return true;
}

// prompt user for new photo name
function getNewPhotoName() {
    var txt;
    var txt = prompt("Add new photo name:");
    if (!isValidPhotoName(txt)) {
        closeNav();
    } else {
        var url = $('.selected-thumbnail').find("img").first().attr('src');
        var imageId = url.split("/").pop();
        sendNewPhotoName(url, imageId, txt);
    }
}

// request update for new photo name
function sendNewPhotoName(imgURL, imageId, fileName) {
    var req = $.post(imgURL, { id: imageId, fileName: fileName }, "text");
    req.done(function (data) {
        $(".selected-thumbnail").find("h5").first().text(data);
        closeNav();
    });

    req.fail(function () {
        alert("Something went wrong on posting new image name");
        closeNav();
    });
}
*/

// When Add to Favorties button is clicked.
fav_btns = document.getElementsByClassName("fav_btn");
for (var i = 0; i < fav_btns.length; i++) {
    fav_btns[i].addEventListener("click", function () {

        let collection = $(this).attr('id');
        let visiteduser = $(this).attr('value');
        let activeuser = $(this).attr('name');
        

        let address = "/Collections/" + activeuser + "/AddFavorite";

        $.ajax({
            type: "Post",
            dataType: "json",
            url: address,
            data: {
                collection: collection,
                visiteduser: visiteduser,
                activeuser: activeuser
            },
            success: addFavorite,
            error: errorOnAjax
        });
    });
}


function addFavorite(data) {

    $("#" + data.collection).addClass('Active');
    $("#" + data.collection).text(data.result);
    $("#" + data.collection).addClass('btn-primary');
    $("#" + data.collection).removeClass('btn-outline-primary');
    $("#" + data.collection).attr({disabled:true});

}
function errorOnAjax() {
    console.log("ERROR in ajax request");
}


//MyCollection visibility checkboxes.
pvt_btns = document.getElementsByClassName("pvt_btn");
for (var i = 0; i < pvt_btns.length; i++) {
    pvt_btns[i].addEventListener("click", function () {

        let collection = $(this).attr('id');
        let visibility = $(this).is(':checked');
        let activeuser = $(this).attr('name');

        let address = "/Collections/" + activeuser + "/Mycollection/MakePrivate";

        $.ajax({
            type: "Post",
            dataType: "json",
            url: address,
            data: {
                collection: collection,
                visibility: visibility,
                activeuser: activeuser
            },
            error: errorOnAjax
        });

    });
}


//Remove favorites button
rem_fav_btns = document.getElementsByClassName("rem_fav");
for (var i = 0; i < rem_fav_btns.length; i++) {
    rem_fav_btns[i].addEventListener("click", function () {

        let collection = $(this).attr('id');
        let username = $(this).attr('name');

        let address = "/Collections/" + username + "/RemoveFavorite";

        $.ajax({
            type: "Post",
            dataType: "json",
            url: address,
            data: {
                collection: collection,
                username: username
            },
            success: FavoriteRemoved,
            error: errorOnAjax
        });

    });
}

function FavoriteRemoved(data)
{
    window.location.reload(true);
}

//Follow button alert for unauthenticated visitor
$("#follow_btn_visitor").click(function () {

    $("#follow_btn_warning").addClass('alert');
    $("#follow_btn_warning").addClass('alert-warning');
    $("#follow_btn_warning").addClass('alert-dismissible');
    $("#follow_btn_warning").addClass('fade');
    $("#follow_btn_warning").addClass('show');
    $("#follow_btn_warning").html("You need to <a href='../Identity/Account/Login'>log in</a> or <a href='../Identity/Account/Register'>Register</a> to follow a user!");

});







