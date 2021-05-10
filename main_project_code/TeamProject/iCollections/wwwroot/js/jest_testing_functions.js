
function sum(a, b) {
    return a + b;
}



function practiceInit(sampleData) {
    var copyData = sampleData
    var sortedInputParam = copyData.slice().sort();
    var outputData = []
    for (var i = 0; i < sortedInputParam.length; i++) {
        outputData[i] = { Data: sortedInputParam[i].Data, Description: sortedInputParam[i].Description }
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

export { sum, practiceInit, GatherPhotoData }