import * as THREE from './three.module.js';

function sum(a, b)
{
    return a + b;
}


    
function practiceInit(sampleData)
{
    var copyData = sampleData
    var sortedInputParam = copyData.slice().sort();
    var outputData = []
    for(var i = 0; i < sortedInputParam.length; i++){
        outputData[i] = { Data : sortedInputParam[i].Data, Description : sortedInputParam[i].Description }
    }
    return outputData;
}  


function GatherPhotoData(photoData) {
    $("tr").each(function () {
        photoData.push({
            srcData: $(this).attr("data-photodata"),
            srcTitle: $(this).attr("data-title"),
            srcRank: $(this).attr("data-rank"),
            srcDescription: $(this).attr("data-description")
        });
    });
    photoData.sort(function (a, b) {
        return a.srcRank - b.srcRank;
    });
    return photoData;
}


//Custom Upload Photo
function uploadImage(collectionPhoto) {
    // create a canvas element
    var canvas = document.createElement('canvas');
    canvas.width = 500;
    canvas.height = 500;
    var context = canvas.getContext('2d');
    // canvas contents will be used for a texture
    var texture = new THREE.Texture(canvas);

    // load an image
    var imageObj = new Image();
    let newSrc = "data:image/png;base64," + collectionPhoto;
    imageObj.src = newSrc;
    // after the image is loaded, this function executes
    imageObj.onload = function () {
        context.drawImage(imageObj, 0, 0, imageObj.width, imageObj.height, 0, 0, 500, 500);
        if (texture) // checks if texture exists
            texture.needsUpdate = true;
    };

    var material = new THREE.MeshBasicMaterial({ map: texture, side: THREE.DoubleSide });
    material.transparent = true;

    var mesh = new THREE.Mesh(
        new THREE.PlaneGeometry(canvas.width, canvas.height),
        material
    );
    //scene.add( mesh2 );
    return mesh
}

function LoadImagesToScene(scene, photoData, positionCordinateData) {
    let currentImage;
    for (let i = 0; i < photoData.length; ++i) {
        currentImage = uploadImage(photoData[i].srcData);
        currentImage.position.set(positionCordinateData[i].xAxis, positionCordinateData[i].yAxis, positionCordinateData[i].zAxis);
        scene.add(currentImage);
    }
}

export { sum, practiceInit, GatherPhotoData, LoadImagesToScene }