using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKeywordHolder
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
}
