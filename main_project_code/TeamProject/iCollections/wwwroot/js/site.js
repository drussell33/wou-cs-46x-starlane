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

// check if photo is more than 1 MB and error if so
// $("#register").submit(function (event) {


//     event.preventDefault();
// });



//// For un-following users 
//$('#unfollow-button > button').click(function () {
//    var followID = this.id.substring(1);      // remove leading 'f'

//    $.ajax({
//        url: 'following',
//        data: { id: followID },
//        method: 'POST',
//        success: updateFollowees
//    });
//});

//function updateFollowees(data) {

//    location.reload(true);
//}

//// For following users 
//$('#follow-button > button').click(function () {
//    var followID = this.id;
//    var followName = this.name;

//    $.ajax({
//        url: 'userpage/follow',
//        data: { id: followID, status: followName },
//        method: 'POST',
//        success: updateFollowees
//    });
//});

//function updateFollowees(data) {

//    location.reload(true);
//}

/*Populate Dropdown Menu With Logged-in User Profile

$(document).ready(function () {
    let url = window.location.origin + "/api/sessionuser";
    if ($("#myProfileLink").length) {
        $.ajax({
            url: url,
            method: 'GET',
            success: updateProfileLink
        })
    }
});

function updateProfileLink(data) {
    let base_url = window.location.origin;
    $("#myProfileLink").attr("href", base_url + "/userpage/" + data.username);
}*/

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

function getNewPhotoName() {
    var txt;
    var txt = prompt("Add new photo name:");
    if (txt == null || txt == "") {
        // User cancelled the prompt -> ignore
    } 
    else if (!txt.replace(/\s/g, '').length) {
        // User has either all whitespace -> ignore
    }
    else {
        var url = $('.selected-thumbnail').attr('src');
        // var imageId = url.split("/").pop();
        sendNewPhotoName(url, txt);
    }
    closeNav();
}

function sendNewPhotoName(imgURL, fileName) {
    console.log("url is: " + imgURL + " and filename is: " + fileName);
    var req = $.post(imgURL, fileName, "text");
    req.done(function(data) {
        alert(data);
    });

    req.fail(function() {
        alert("Something went wrong on posting new image name");
    });

    // var jqxhr = $.post( "example.php", function() {
    //     alert( "success" );
    //   })
    //     .done(function() {
    //       alert( "second success" );
    //     })
    //     .fail(function() {
    //       alert( "error" );
    //     })
    //     .always(function() {
    //       alert( "finished" );
    //     });
       
    //   // Perform other work here ...
       
    //   // Set another completion function for the request above
    //   jqxhr.always(function() {
    //     alert( "second finished" );
    //   });
}
