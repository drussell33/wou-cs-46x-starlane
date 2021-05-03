import {GatherPhotoData} from '../../iCollections/wwwroot/js/jest_testing_functions'
//import { screen } from '@testing-library/jest-dom'
//import '@testing-library/jest-dom/extend-expect';
import {toHaveClass} from '@testing-library/jest-dom/matchers'



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
            '  <tr data-title="photo1" data-photodata="NOPENOPENOPE" data-rank=1 data-description="description1" />' +
            '  <tr data-title="photo2" data-photodata="NOPENOPENOPE" data-rank=2 data-description="description2"/>' +
            '  <tr data-title="photo3" data-photodata="NOPENOPENOPE" data-rank=3 data-description="description3"/>' +
            '  <tr data-title="photo4" data-photodata="NOPENOPENOPE" data-rank=4 data-description="description4"/>' +
            '  <tr data-title="photo5" data-photodata="NOPENOPENOPE" data-rank=5 data-description="description5"/>' +
            '  <tr data-title="photo6" data-photodata="NOPENOPENOPE" data-rank=6 data-description="description6"/>' +
            '  <tr data-title="photo7" data-photodata="NOPENOPENOPE" data-rank=7 data-description="description7"/>' +
            '  <tr data-title="photo8" data-photodata="NOPENOPENOPE" data-rank=8 data-description="description8"/>' +
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

//import { screen } from '@testing-library/dom'

test.skip('uses jest-dom', () => {
  document.body.innerHTML = `
    <span data-testid="not-empty"><span data-testid="empty"></span></span>
    <div data-testid="visible">Visible Example</div>
  `

  expect(screen.queryByTestId('not-empty')).not.toBeEmptyDOMElement()
  expect(screen.getByText('Visible Example')).toBeVisible()
})