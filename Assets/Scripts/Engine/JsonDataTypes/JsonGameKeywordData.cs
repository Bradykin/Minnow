using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JsonGameKeywordData
{
    //GameElementBase values
    public string name;

    //Json Keyword parsing data
    public int intValue;
    public bool boolValue;
    public List<JsonGameActionData> gameActionData;
}
