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
    public string m_focusInfoText = "Focus Info Text";
    public string m_shortDesc = string.Empty;
    public bool m_isVisible = true;

    public abstract void AddKeyword(GameKeywordBase toAdd);

    public abstract string SaveToJsonAsString();

    public abstract void LoadFromJson(JsonKeywordData jsonData);

    public virtual string GetFocusInfoText()
    {
        return m_focusInfoText;
    }
}
