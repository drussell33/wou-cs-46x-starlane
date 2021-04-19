import {practiceInit} from '../../iCollections/wwwroot/js/sum'
//const sum = require('./sum');

test('CanvasObject_ShouldCreateAndModify_True', () => {
    //Arrange
    var canvas = document.createElement('canvas');
    //Act
    canvas.width = 150;
    //Assert
    expect(150).toBe(canvas.width);
});

test('ImageObject_ShouldCreateAndModify_True', () => {
    //Arrange
    var imageObj = new Image();
    imageObj.src = './images/fish_pics/fish4.png';
    //Act
    imageObj.width = 500;
    //Assert
    expect(500).toBe(imageObj.width);
});

  var bytes = new Uint8Array(1024);
  let sampleData = [
    {
        "Data": bytes,
        "Title": "title0",
        "PhotoRank": 0,
        "Description": "description0"
    },
    {
        "Data": bytes,
        "Title": "title1",
        "PhotoRank": 1,
        "Description": "description1"
    },
    {
        "Data": bytes,
        "Title": "title2",
        "PhotoRank": 2,
        "Description": "description2"
    },
    {
        "Data": bytes,
        "Title": "title3",
        "PhotoRank": 3,
        "Description": "description3" 
    },
    {
        "Data": bytes,
        "Title": "title4",
        "PhotoRank": 4,
        "Description": "description4"
    },
    {
        "Data": bytes,
        "Title": "title5",
        "PhotoRank": 5,
        "Description": "description5"
    },
    {
        "Data": bytes,
        "Title": "title6",
        "PhotoRank": 6,
        "Description": "description6"
    },
    {
        "Data": bytes,
        "Title": "title7",
        "PhotoRank": 7,
        "Description": "description7"
    },
    ];
    
  test('CreatingArrayParamThatReturnsWholeArray_SHouldLoadArrayAsParamToFunctionAndTestLength_True', () => {
    //Arrange
    var inputParam = practiceInit(sampleData);
    //Act
    var testLength = inputParam.length;
    //Assert
    expect(8).toBe(testLength);
});

test('CreatingArrayParamThatReturnsWholeArray_SHouldNotBeofLength9_True', () => {
    //Arrange
    var inputParam = practiceInit(sampleData);
    //Act
    var testLength = inputParam.length;
    //Assert
    expect(9).not.toBe(testLength);
    expect(7).not.toBe(testLength);
    expect(0).not.toBe(testLength);
});

test('CreatingArrayParamThatReturnsWholeArray_SHouldLoadArrayAsParamToFunctionAndContainValue_True', () => {
    //Arrange
    var inputParam = practiceInit(sampleData);
    //Act

    //Assert
    expect(inputParam[6]["Description"]).toContain("description6");
});

test('CreatingNewArrayWithSort_SHouldLoadArrayFROMParamToFunctionAndContainValueInOrder_True', () => {
    //Arrange
    var inputParam = practiceInit(sampleData);
    //Act
    var sortedInputParam = inputParam.slice().sort();
    //Assert
    expect(sortedInputParam[6]["Description"]).toContain("description6");
});

test('CheckingOutputofNewArray_ShouldCopyDescriptionAndReturn_True', () => {
    //Arrange
    var inputParam = practiceInit(sampleData);
    //Act
    var sortedInputParam = inputParam.slice().sort();
    //Assert
    expect(sortedInputParam[6]["Description"]).toContain("description6");
});


test('CheckingOmitedDataFIelds_ShouldReturnTrueforObmitingPhotoRank_True', () => {
    //Arrange
    var inputParam = practiceInit(sampleData);
    //Act
    var byteArray = new Uint8Array(1024);
    //Assert
    expect(inputParam[6]).not.toContain('["PhotoRank"]');
    //expect(inputParam[6]['Data']).toContain('notreal6');
    expect(inputParam[6]['Data']).toBeInstanceOf(byteArray);
});
