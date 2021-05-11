// when a photo is clicked in view photos, highlight it
$('.pic-thumbnail').click(function (e) {
    if (e.ctrlKey) { }
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