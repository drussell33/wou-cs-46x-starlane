function sum(a,b)
{
    return a + b;
}

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

export { sum, practiceInit }