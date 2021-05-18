// when a photo is clicked in view photos, highlight it
$('.pic-thumbnail').click(function (e) {
    if (e.ctrlKey) { }
    else {
        $('.selected-thumbnail').removeClass('selected-thumbnail');
        $(this).find("img").addClass('selected-thumbnail');
    }
});

/* Set the width of the side navigation to 250px */
$(".rename-btn").on("click", function() {
    document.getElementById("mySidenav").style.width = "250px";
})

/* Set the width of the side navigation to 0 */
function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    $('.selected-thumbnail').removeClass('selected-thumbnail');
}

// expand upload options
$("#uploadNewPhoto").on("click", function () {
    let e = $(this);
    let item = e.siblings("div.my-hidden-object").first();
    if (!item.is(":visible")) {
        item.show();
    }
    else {
        item.hide();
    }
})

// expand photo options
$(".gallery-picture").on("click", function () {
    let e = $(this);
    let item = e.siblings("div.my-hidden-object").first();
    if (!item.is(":visible")) {
        item.show();
    }
    else {
        item.hide();
    }
})

function isValidPhotoName(proposed) {
    if (proposed === null || proposed === "") {
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
$("#submitRename").on("click", function () {
    let e = $(this);
    let input = e.prev();
    let txt = input.val();
    let validation = $("#filename-validation");

    if (!isValidPhotoName(txt)) {
        validation.css("color", "red");
        validation.text("Filename is not valid!");
    } else {
        validation.text("");
        let url = $('.selected-thumbnail').attr('src');
        console.log(url);
        let imageId = url.split("/").pop();
        sendNewPhotoName(url, imageId, txt);
    }
})

// request update for new photo name
function sendNewPhotoName(imgURL, imageId, fileName) {
    var req = $.post(imgURL, { id: imageId, fileName: fileName }, "text");
    req.done(function (data) {
        $(".selected-thumbnail").next().find("h5").text(data);
        closeNav();
    });

    req.fail(function () {
        alert("Something went wrong on posting new image name");
        closeNav();
    });
}

// delete photo
$(".delete-btn").on("click", function () {
    let e = $(this);
    let hidden = e.parent();
    let photo = hidden.siblings("img").first();
    let container = hidden.parent().parent();
    let address = "/api/deletePhoto";

    $.ajax({
        type: "post",
        dataType: "json",
        url: address,
        data: {
            photoId: photo.attr('value')
        },
        success: function (data) {
            container.remove();
        }
    });
})
