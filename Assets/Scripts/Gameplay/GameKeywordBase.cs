﻿using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameKeywordBase : GameElementBase, ISave<JsonGameKeywordData>, ILoad<JsonGameKeywordData>
{
    public enum KeywordParamType : int
    {
        NoParams,
        IntParam,
        BoolParam,
        IntBoolParam,
        ActionParam
    }

    public KeywordParamType m_keywordParamType;
    public string m_focusInfoText = "Focus Info Text";
    public string m_shortDesc = string.Empty;
    public bool m_isVisible = true;
    public bool m_isPermanent = false;

    public abstract string GetDesc();

    public virtual string GetDisplayString()
    {
        string displayString = "";

        if (!m_isVisible)
        {
            return string.Empty;
        }

        displayString += "<b>" + GetName() + "</b>";
        if (m_shortDesc != string.Empty)
        {
            displayString += " <i>(" + m_shortDesc + ")</i>";
        }
        if (GetDesc() != string.Empty)
        {
            displayString += ": " + GetDesc();
        }

        return displayString;
    }

    public abstract void AddKeyword(GameKeywordBase toAdd);

    public abstract void SubtractKeyword(GameKeywordBase toSubtract);

    public abstract bool ShouldBeRemoved();

    public virtual string GetFocusInfoText()
    {
        return m_focusInfoText;
    }

    public abstract JsonGameKeywordData SaveToJson();

    public abstract void LoadFromJson(JsonGameKeywordData jsonData);
}
