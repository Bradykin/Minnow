using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKeywordHolder : ISave, ILoad<(JsonKeywordHolderData, GameEntity)>
{
    public List<GameKeywordBase> m_keywords;

    public GameKeywordHolder()
    {
        m_keywords = new List<GameKeywordBase>();
    }

    public GameKeywordHolder Clone(GameEntity gameEntity)
    {
        GameKeywordHolder newHolder = new GameKeywordHolder();

        for (int i = 0; i < m_keywords.Count; i++)
        {
            newHolder.m_keywords.Add(GameKeywordFactory.GetKeywordClone(m_keywords[i], gameEntity));
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

    public void RemoveKeyword(GameKeywordBase toRemove)
    {
        m_keywords.Remove(toRemove);
    }

    public string GetDesc()
    {
        string descString = "";

        for (int i = 0; i < m_keywords.Count; i++)
        {
            descString += "<b>" + m_keywords[i].m_name + "</b>: " + m_keywords[i].m_desc + "\n";
        }

        return descString;
    }

    public string SaveToJson()
    {
        JsonKeywordHolderData jsonData = new JsonKeywordHolderData
        {
            keywordJson = new List<string>()
        };

        foreach (GameKeywordBase keyword in m_keywords)
        {
            jsonData.keywordJson.Add(keyword.SaveToJson());
        }

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public void LoadFromJson((JsonKeywordHolderData, GameEntity) tuple)
    {
        foreach (string keywordJson in tuple.Item1.keywordJson)
        {
            JsonKeywordData keywordData = JsonUtility.FromJson<JsonKeywordData>(keywordJson);
            m_keywords.Add(GameKeywordFactory.GetKeywordsFromJson(keywordData, tuple.Item2));
        }
    }
}
