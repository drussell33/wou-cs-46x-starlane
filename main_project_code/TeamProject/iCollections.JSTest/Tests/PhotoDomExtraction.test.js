import {GatherPhotoData} from '../../iCollections/wwwroot/js/jest_testing_functions'
import {toHaveClass} from '@testing-library/jest-dom/matchers'

//Derek Russell
//User Story ID: 177878958, Sprint 5, 2 Points.

    test('GatherPhotoData_MakeingFakeDOMWithRowsForFunctionToFind_mightWork', () => {
        //Arrange
        document.body.innerHTML =
            '<table>' +
            '  <tr id="photo1" />' +
            '  <tr id="photo2" />' +
            '</table>';
        //Act
        var photoData = [];
        photoData = GatherPhotoData(photoData);
        var testLength = photoData.length;
        //Assert
        expect(2).toBe(testLength);       
    });

    test('GatherPhotoData_MakeingFakeDOMWithRowsForFunctionToFind_mightWorkAgain', () => {
        //Arrange
        document.body.innerHTML =
            '<table>' +
            '  <tr data-title="photo1" data-photodata="NotRealAndNotNeededForThisTest" data-rank=1 data-description="description1" />' +
            '  <tr data-title="photo2" data-photodata="NotRealAndNotNeededForThisTest" data-rank=2 data-description="description2"/>' +
            '  <tr data-title="photo3" data-photodata="NotRealAndNotNeededForThisTest" data-rank=3 data-description="description3"/>' +
            '  <tr data-title="photo4" data-photodata="NotRealAndNotNeededForThisTest" data-rank=4 data-description="description4"/>' +
            '  <tr data-title="photo5" data-photodata="NotRealAndNotNeededForThisTest" data-rank=5 data-description="description5"/>' +
            '  <tr data-title="photo6" data-photodata="NotRealAndNotNeededForThisTest" data-rank=6 data-description="description6"/>' +
            '  <tr data-title="photo7" data-photodata="NotRealAndNotNeededForThisTest" data-rank=7 data-description="description7"/>' +
            '  <tr data-title="photo8" data-photodata="NotRealAndNotNeededForThisTest" data-rank=8 data-description="description8"/>' +
            '</table>';
        //Act
        var photoData = [];
        photoData = GatherPhotoData(photoData);
        var testLength = photoData.length;
        //Assert
        expect(8).toBe(testLength);       
    });

    
    test('GatherPhotoData_TestingAJestDOMLIbraryMethodtoHaveClass_mightWorkAgain', () => {
        
        document.body.innerHTML = `
        <h1 class="find-me">Hello world!</h1>
        <tr>Table Row! Whaaat</tr>
      `
    
        let h1 = document.querySelector('h1');
    
        expect(h1).toHaveClass('find-me');      
    });



test('testing output of global should be true', () => {
    expect(Derek).toBe("derek");

});

test('testing output of global should be false', () => {
    expect(Derek).not.toBe("someone else");

});


test('testingDOMCreationinTest_works', () => {
    document.body.innerHTML = `
    <h1>Hello world!</h1>
    <tr>Table Row! Whaaat</tr>
  `

    let h1 = document.querySelector('h1');

    expect(h1.textContent).toEqual('Hello world!');
});


test('GatherPhotoData_TestingThatPhotosOrganizedByRankAsOutput_mightWorkAgain', () => {
    //Arrange
    document.body.innerHTML =
        '<table>' +
        '  <tr data-title="photo1" data-photodata="NotRealAndNotNeededForThisTest" data-rank=1 data-description="description1" />' +
        '  <tr data-title="photo2" data-photodata="NotRealAndNotNeededForThisTest" data-rank=7 data-description="description2"/>' +
        '  <tr data-title="photo3" data-photodata="NotRealAndNotNeededForThisTest" data-rank=3 data-description="description3"/>' +
        '  <tr data-title="photo4" data-photodata="NotRealAndNotNeededForThisTest" data-rank=4 data-description="description4"/>' +
        '  <tr data-title="photo5" data-photodata="NotRealAndNotNeededForThisTest" data-rank=5 data-description="description5"/>' +
        '  <tr data-title="photo6" data-photodata="NotRealAndNotNeededForThisTest" data-rank=8 data-description="description6"/>' +
        '  <tr data-title="photo7" data-photodata="NotRealAndNotNeededForThisTest" data-rank=6 data-description="description7"/>' +
        '  <tr data-title="photo8" data-photodata="NotRealAndNotNeededForThisTest" data-rank=2 data-description="description8"/>' +
        '</table>';
    //Act
    var photoData = [];
    photoData = GatherPhotoData(photoData);

    //Assert
    expect(photoData[0]["srcRank"]).toBe('1');  
    expect(photoData[1]["srcRank"]).toBe('2');  
    expect(photoData[2]["srcRank"]).toBe('3');  
    expect(photoData[3]["srcRank"]).toBe('4');  
    expect(photoData[4]["srcRank"]).toBe('5');   
    expect(photoData[5]["srcRank"]).toBe('6'); 
    expect(photoData[6]["srcRank"]).toBe('7'); 
    expect(photoData[7]["srcRank"]).toBe('8'); 
});

test('GatherPhotoData_TestingThatPhotosOrganizedByRankForTitle_SHouldWork', () => {
    //Arrange
    document.body.innerHTML =
        '<table>' +
        '  <tr data-title="photo1" data-photodata="NotRealAndNotNeededForThisTest" data-rank=1 data-description="description1" />' +
        '  <tr data-title="photo2" data-photodata="NotRealAndNotNeededForThisTest" data-rank=7 data-description="description2"/>' +
        '  <tr data-title="photo3" data-photodata="NotRealAndNotNeededForThisTest" data-rank=3 data-description="description3"/>' +
        '  <tr data-title="photo4" data-photodata="NotRealAndNotNeededForThisTest" data-rank=4 data-description="description4"/>' +
        '  <tr data-title="photo5" data-photodata="NotRealAndNotNeededForThisTest" data-rank=5 data-description="description5"/>' +
        '  <tr data-title="photo6" data-photodata="NotRealAndNotNeededForThisTest" data-rank=8 data-description="description6"/>' +
        '  <tr data-title="photo7" data-photodata="NotRealAndNotNeededForThisTest" data-rank=6 data-description="description7"/>' +
        '  <tr data-title="photo8" data-photodata="NotRealAndNotNeededForThisTest" data-rank=2 data-description="description8"/>' +
        '</table>';
    //Act
    var photoData = [];
    photoData = GatherPhotoData(photoData);

    //Assert
    expect(photoData[0]["srcTitle"]).toBe('photo1');  
    expect(photoData[1]["srcTitle"]).toBe('photo8');  
    expect(photoData[2]["srcTitle"]).toBe('photo3');  
    expect(photoData[3]["srcTitle"]).toBe('photo4');  
    expect(photoData[4]["srcTitle"]).toBe('photo5');   
    expect(photoData[5]["srcTitle"]).toBe('photo7'); 
    expect(photoData[6]["srcTitle"]).toBe('photo2'); 
    expect(photoData[7]["srcTitle"]).toBe('photo6'); 
});

test('GatherPhotoData_TestingThatPhotosOrganizedByRankForTitle_SHouldWork', () => {
    //Arrange
    document.body.innerHTML =
        '<table>' +
        '  <tr data-title="photo1" data-photodata="NotRealAndNotNeededForThisTest" data-rank=1 data-description="description1" />' +
        '  <tr data-title="photo2" data-photodata="NotRealAndNotNeededForThisTest" data-rank=7 data-description="description2"/>' +
        '  <tr data-title="photo3" data-photodata="NotRealAndNotNeededForThisTest" data-rank=3 data-description="description3"/>' +
        '  <tr data-title="photo4" data-photodata="NotRealAndNotNeededForThisTest" data-rank=4 data-description="description4"/>' +
        '  <tr data-title="photo5" data-photodata="NotRealAndNotNeededForThisTest" data-rank=5 data-description="description5"/>' +
        '  <tr data-title="photo6" data-photodata="NotRealAndNotNeededForThisTest" data-rank=8 data-description="description6"/>' +
        '  <tr data-title="photo7" data-photodata="NotRealAndNotNeededForThisTest" data-rank=6 data-description="description7"/>' +
        '  <tr data-title="photo8" data-photodata="NotRealAndNotNeededForThisTest" data-rank=2 data-description="description8"/>' +
        '</table>';
    //Act
    var photoData = [];
    photoData = GatherPhotoData(photoData);

    //Assert
    expect(photoData[0]["srcDescription"]).toBe('description1');  
    expect(photoData[1]["srcDescription"]).toBe('description8');  
    expect(photoData[2]["srcDescription"]).toBe('description3');  
    expect(photoData[3]["srcDescription"]).toBe('description4');  
    expect(photoData[4]["srcDescription"]).toBe('description5');   
    expect(photoData[5]["srcDescription"]).toBe('description7'); 
    expect(photoData[6]["srcDescription"]).toBe('description2'); 
    expect(photoData[7]["srcDescription"]).toBe('description6'); 
});


test('GatherPhotoData_ZeroItemsWillPassEmptyArrayThrough_SHouldWork', () => {
    //Arrange
    document.body.innerHTML =
        '<table>' +
        '</table>';
    //Act
    var photoData = [];
    photoData = GatherPhotoData(photoData);

    //Assert
    expect(photoData).toStrictEqual([]);  

});

test('GatherPhotoData_ZeroItemsWillPassEmptyArrayThrough_SHouldWork', () => {
    //Arrange
    document.body.innerHTML =
        '<table>' +
        '</table>';
    //Act
    var photoData = [];
    photoData = GatherPhotoData(photoData);

    //Assert
    expect(photoData).toStrictEqual([]);  

});