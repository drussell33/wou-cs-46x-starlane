function sum(a,b)
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

export { sum, practiceInit }