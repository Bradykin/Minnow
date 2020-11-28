using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKeywordHolder : ISave<JsonGameKeywordHolderData>, ILoad<(JsonGameKeywordHolderData, GameUnit)>
{
    private List<GameKeywordBase> m_keywords;

    public GameKeywordHolder()
    {
        m_keywords = new List<GameKeywordBase>();
    }

    public GameKeywordHolder Clone(GameUnit gameUnit, GameUnit cloneTo)
    {
        GameKeywordHolder newHolder = new GameKeywordHolder();

        for (int i = 0; i < m_keywords.Count; i++)
        {
            newHolder.m_keywords.Add(GameKeywordFactory.GetKeywordClone(m_keywords[i], cloneTo));
        }

        return newHolder;
    }

    public T GetKeyword<T>()
    {
        for (int i = 0; i < m_keywords.Count; i++)
        {
            if (m_keywords[i] is T val)
            {
                return val;
            }
        }

        return default(T);
    }

    public List<T> GetKeywords<T>()
    {
        List<T> listOfKeyword = new List<T>();
        for (int i = 0; i < m_keywords.Count; i++)
        {
            if (m_keywords[i] is T val)
            {
                listOfKeyword.Add(val);
            }
        }

        return listOfKeyword;
    }

    public IReadOnlyList<GameKeywordBase> GetKeywordsForRead()
    {
        return m_keywords;
    }

    public void AddKeyword(GameKeywordBase newKeyword)
    {
        //If there are any keywords that are the same as the one being added; instead of adding a new one, add this one to that keyword
        for (int i = 0; i < m_keywords.Count; i++)
        {
            if (m_keywords[i].GetName() == newKeyword.GetName())
            {
                m_keywords[i].AddKeyword(newKeyword);
                return;
            }
        }

        m_keywords.Add(newKeyword);
    }

    public void RemoveKeyword(GameKeywordBase toRemove)
    {
        m_keywords.Remove(toRemove);
    }

    public void SubtractKeyword(GameKeywordBase toSubtract)
    {
        for (int i = 0; i < m_keywords.Count; i++)
        {
            if (m_keywords[i].GetName() == toSubtract.GetName())
            {
                switch(m_keywords[i].m_keywordParamType)
                {
                    case GameKeywordBase.KeywordParamType.NoParams:
                        m_keywords.RemoveAt(i);
                        break;
                    case GameKeywordBase.KeywordParamType.IntParam:
                    case GameKeywordBase.KeywordParamType.IntBoolParam:
                    case GameKeywordBase.KeywordParamType.ActionParam:
                        m_keywords[i].SubtractKeyword(toSubtract);
                        if (m_keywords[i].ShouldBeRemoved())
                        {
                            m_keywords.RemoveAt(i);
                        }
                        break;
                }
                return;
            }
        }
    }

    public void RemoveAllKeywords(bool ignorePerm)
    {
        if (!ignorePerm)
        {
            m_keywords.Clear();
        }
        else
        {
            for (int i = m_keywords.Count-1; i >= 0; i--)
            {
                if (!m_keywords[i].m_isPermanent)
                {
                    m_keywords.RemoveAt(i);
                }
            }
        }
    }

    public void RemoveAllVisibleKeywords()
    {
        for (int i = m_keywords.Count - 1; i >= 0; i--)
        {
            if (m_keywords[i].m_isVisible)
            {
                m_keywords.RemoveAt(i);
            }
        }
    }

    public string GetDesc()
    {
        string descString = "";

        for (int i = 0; i < m_keywords.Count; i++)
        {
            if (!m_keywords[i].m_isVisible)
            {
                continue;
            }

            descString += "<b>" + m_keywords[i].GetName() + "</b>";
            if (m_keywords[i].m_shortDesc != string.Empty)
            {
                descString += " <i>(" + m_keywords[i].m_shortDesc + ")</i>";
            }
            if (m_keywords[i].GetDesc() != string.Empty)
            {
                descString += ": " + m_keywords[i].GetDesc();
            }

            if (i != m_keywords.Count-1)
            {
                descString += "\n";
            }
        }

        return descString;
    }

    public int GetNumKeywords()
    {
        return m_keywords.Count;
    }

    public int GetNumVisibleKeywords()
    {
        int numVisibleKeywords = 0;

        for (int i = 0; i < m_keywords.Count; i++)
        {
            if (m_keywords[i].m_isVisible)
            {
                numVisibleKeywords++;
            }
        }

        return numVisibleKeywords;
    }

    public JsonGameKeywordHolderData SaveToJson()
    {
        JsonGameKeywordHolderData jsonData = new JsonGameKeywordHolderData
        {
            gameKeywordData = new List<JsonGameKeywordData>()
        };

        foreach (GameKeywordBase keyword in m_keywords)
        {
            jsonData.gameKeywordData.Add(keyword.SaveToJson());
        }

        return jsonData;
    }

    public void LoadFromJson((JsonGameKeywordHolderData, GameUnit) tuple)
    {
        foreach (JsonGameKeywordData keywordJson in tuple.Item1.gameKeywordData)
        {
            m_keywords.Add(GameKeywordFactory.GetKeywordsFromJson(keywordJson, tuple.Item2));
        }
    }
}
