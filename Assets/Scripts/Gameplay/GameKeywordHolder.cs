using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKeywordHolder : ISave, ILoad<(JsonKeywordHolderData, GameUnit)>
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
        List<T> listOfKeyword = new List<T>();
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

    public List<GameKeywordBase> GetKeywordsForRead()
    {
        return m_keywords;
    }

    public void AddKeyword(GameKeywordBase newKeyword)
    {
        //If there are any keywords that are the same as the one being added; instead of adding a new one, add this one to that keyword
        for (int i = 0; i < m_keywords.Count; i++)
        {
            if (m_keywords[i].m_name == newKeyword.m_name)
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

    public void RemoveAllKeywords()
    {
        m_keywords.Clear();
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

            descString += "<b>" + m_keywords[i].m_name + "</b>";
            if (m_keywords[i].m_shortDesc != string.Empty)
            {
                descString += " <i>(" + m_keywords[i].m_shortDesc + ")</i>";
            }
            if (m_keywords[i].m_desc != string.Empty)
            {
                descString += ": " + m_keywords[i].m_desc;
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

    public string SaveToJsonAsString()
    {
        JsonKeywordHolderData jsonData = new JsonKeywordHolderData
        {
            keywordJson = new List<string>()
        };

        foreach (GameKeywordBase keyword in m_keywords)
        {
            jsonData.keywordJson.Add(keyword.SaveToJsonAsString());
        }

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public void LoadFromJson((JsonKeywordHolderData, GameUnit) tuple)
    {
        foreach (string keywordJson in tuple.Item1.keywordJson)
        {
            JsonKeywordData keywordData = JsonConvert.DeserializeObject<JsonKeywordData>(keywordJson);
            m_keywords.Add(GameKeywordFactory.GetKeywordsFromJson(keywordData, tuple.Item2));
        }
    }
}
