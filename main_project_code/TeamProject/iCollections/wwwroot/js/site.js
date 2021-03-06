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

// check if photo is more than 1 MB and error if so
// $("#register").submit(function (event) {
    

//     event.preventDefault();
// });






// For un-following users 

$('#unfollow-button > button').click(function () {
    var followID = this.id.substring(1);      // remove leading 'f'
    console.log('Button with id = ' + followID + '  clicked');

    $.ajax({
        url: 'following',
        data: { id: followID },
        method: 'POST',
        success: updateFollowees
    });
});

function updateFollowees(data) {

    console.log('Updating Followees');
}



    
    
    
   