// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//    SHOW UPLOADED IMAGE
function readURL(input) {
    console.log(input);
    if (input.files && input.files[0] && input['type'].split('/')[0] === 'image') {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imageResult')
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
    else {
        alert("Please enter an image.")
    }
}

$(function () {
    $('#upload').on('change', function () {
        readURL(input);
    });
});


//    SHOW UPLOADED IMAGE NAME

var input = document.getElementById( 'upload' );
var infoArea = document.getElementById( 'upload-label' );

input.addEventListener( 'change', showFileName );
function showFileName( event ) {
  var input = event.srcElement;
  var fileName = input.files[0].name;
  infoArea.textContent = 'File name: ' + fileName;
}