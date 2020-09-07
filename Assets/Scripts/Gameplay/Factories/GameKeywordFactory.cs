using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKeywordFactory
{
    private static List<GameKeywordBase> m_keywords = new List<GameKeywordBase>();

    private static bool m_hasInit = false;

    public static void Init()
    {
        m_keywords.Add(new GameDeathKeyword(null));
        m_keywords.Add(new GameEnrageKeyword(null));
        m_keywords.Add(new GameFlyingKeyword());
        m_keywords.Add(new GameKnowledgeableKeyword(null));
        m_keywords.Add(new GameMomentumKeyword(null));
        m_keywords.Add(new GameRangeKeyword(0));
        m_keywords.Add(new GameRegenerateKeyword(0));
        m_keywords.Add(new GameSpellcraftKeyword(null));
        m_keywords.Add(new GameSummonKeyword(null));
        m_keywords.Add(new GameVictoriousKeyword(null));
        
        m_hasInit = true;
    }

    public static GameKeywordBase GetKeywordsFromJson(JsonKeywordData jsonData)
    {
        if (!m_hasInit)
            Init();

        int i = m_keywords.FindIndex(t => t.m_name == jsonData.name);

        GameKeywordBase newKeyword = (GameKeywordBase)Activator.CreateInstance(m_keywords[i].GetType());
        newKeyword.LoadFromJson(jsonData);

        return newKeyword;
    }
}
