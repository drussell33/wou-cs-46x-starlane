// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
        showFileName();
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

function showFileName() {
    var fileName = input.files[0].name;
    infoArea.textContent = 'File name: ' + fileName;
}

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

// when a photo is clicked in view photos, highlight it
$('.pic-thumbnail').click(function (e) {
    if (e.ctrlKey) {}
    else {
        $('.selected-thumbnail').removeClass('selected-thumbnail');
        $(this).addClass('selected-thumbnail');
    }
});

/* Set the width of the side navigation to 250px */
function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
}

/* Set the width of the side navigation to 0 */
function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    $('.selected-thumbnail').removeClass('selected-thumbnail');
}

// prompt user for new photo name
function getNewPhotoName() {
    var txt;
    var txt = prompt("Add new photo name:");
    if (txt == null || txt == "") {
        // User cancelled the prompt -> ignore
        closeNav();
    } 
    else if (!txt.replace(/\s/g, '').length) {
        // User has either all whitespace -> tell user
        alert("Enter a non-empty name");
        closeNav();
    }
    else {
        var url = $('.selected-thumbnail').find("img").first().attr('src');
        var imageId = url.split("/").pop();
        sendNewPhotoName(url, imageId, txt);
    }
}

// request update for new photo name
function sendNewPhotoName(imgURL, imageId, fileName) {
    var req = $.post(imgURL, { id : imageId, fileName: fileName }, "text");
    req.done(function(data) {
        $(".selected-thumbnail").find("h5").first().text(data);
        closeNav();
    });

    req.fail(function() {
        alert("Something went wrong on posting new image name");
        closeNav();
    });
}
