using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JsonGameActionData
{
    //GameElementBase values
    public string name;

    //Json Keyword parsing data
    public int intValue1;
    public int intValue2;
    public bool boolValue1;
    public List<int> intListValue1;

    public JsonGameKeywordData gameKeywordData;
    public string gameWalletJsonValue;
}
