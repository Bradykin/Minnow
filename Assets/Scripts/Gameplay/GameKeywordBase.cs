using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameKeywordBase : GameElementBase, ISave, ILoad<JsonKeywordData>
{
    public enum KeywordParamType : int
    {
        NoParams,
        IntParam,
        TwoIntParam,
        ActionParam
    }

    public KeywordParamType m_keywordParamType;

    public abstract string SaveToJson();

    public abstract void LoadFromJson(JsonKeywordData jsonData);
}
