using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JsonActionData
{
    //GameElementBase values
    public string name;

    //Json Keyword parsing data
    public int intValue1;
    public int intValue2;
    public List<int> intListValue1;

    public JsonKeywordData keywordValue;
    public string gameWalletJsonValue;
}
