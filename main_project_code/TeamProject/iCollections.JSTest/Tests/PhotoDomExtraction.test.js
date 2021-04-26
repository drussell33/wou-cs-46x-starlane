import {GatherPhotoData} from '../../iCollections/wwwroot/js/jest_testing_functions'




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
    
  
    test.skip('GatherPhotoData_MakeingFakeDOMWithRowsForFunctionToFind_mightWork', () => {
        //Arrange
        document.body.innerHTML =
            '<table>' +
            '  <tr id="photo1" />' +
            '  <tr id="photo2" />' +
            '</table>';
        //Act
        var photoData = [];
        //const $ = require('../../iCollections/wwwroot/lib/jquery/dist/jquery.min.js');
        //import * as $ from '../../iCollections/wwwroot/lib/jquery/dist/jquery.min.js'
        photoData = GatherPhotoData(photoData);
        var testLength = photoData.length;
        //Assert
        expect(2).toBe(testLength);       
    });