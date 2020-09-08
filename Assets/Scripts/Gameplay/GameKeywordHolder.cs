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

    public string GetDesc()
    {
        string descString = "";

        for (int i = 0; i < m_keywords.Count; i++)
        {
            descString += "<b>" + m_keywords[i].m_name + "</b>  ";
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
