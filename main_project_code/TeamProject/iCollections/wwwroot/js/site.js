// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//    SHOW UPLOADED IMAGE
function readURL(input) {
    console.log(input['type']);
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imageResult')
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
        showFileName();
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