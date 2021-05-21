import {practiceInit} from '../../iCollections/wwwroot/js/jest_testing_functions'
//const sum = require('./sum');
//Derek Sprint 4
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


const shoppingList = [
    'diapers',
    'kleenex',
    'trash bags',
    'paper towels',
    'beer',
  ];
  
  test('JestSampleArrayReading', () => {
    expect(shoppingList).toContain('beer');
    expect(new Set(shoppingList)).toContain('beer');
  });

  let sampleData = [
    {
        "Data": "notreal0",
        "Title": "title0",
        "PhotoRank": 0,
        "Description": "description0"
    },
    {
        "Data": "notreal1",
        "Title": "title1",
        "PhotoRank": 1,
        "Description": "description1"
    },
    {
        "Data": "notreal2",
        "Title": "title2",
        "PhotoRank": 2,
        "Description": "description2"
    },
    {
        "Data": "notreal3",
        "Title": "title3",
        "PhotoRank": 3,
        "Description": "description3" 
    },
    {
        "Data": "notreal4",
        "Title": "title4",
        "PhotoRank": 4,
        "Description": "description4"
    },
    {
        "Data": "notreal5",
        "Title": "title5",
        "PhotoRank": 5,
        "Description": "description5"
    },
    {
        "Data": "notreal6",
        "Title": "title6",
        "PhotoRank": 6,
        "Description": "description6"
    },
    {
        "Data": "notreal7",
        "Title": "title7",
        "PhotoRank": 7,
        "Description": "description7"
    },
    ];
    
    // function practiceInit(sampleData)
    // {
    //     var copyData = sampleData
    //     return copyData;
    // }   

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
    
    //Assert
    expect(inputParam[6]).not.toContain('["PhotoRank"]');
    expect(inputParam[6]['Data']).toContain('notreal6');
});
