using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
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
        m_keywords.Add(new GameWaterwalkKeyword());
        m_keywords.Add(new GameMountainwalkKeyword());
        m_keywords.Add(new GameKnowledgeableKeyword(null));
        m_keywords.Add(new GameMomentumKeyword(null));
        m_keywords.Add(new GameRangeKeyword(0));
        m_keywords.Add(new GameBrittleKeyword(0));
        m_keywords.Add(new GameDamageShieldKeyword(0));
        m_keywords.Add(new GameRegenerateKeyword(0));
        m_keywords.Add(new GameSpellcraftKeyword(null));
        m_keywords.Add(new GameSummonKeyword(null));
        m_keywords.Add(new GameVictoriousKeyword(null));
        
        m_hasInit = true;
    }

    public static GameKeywordBase GetKeywordsFromJson(JsonKeywordData jsonData, GameEntity gameEntity)
    {
        if (!m_hasInit)
            Init();

        int i = m_keywords.FindIndex(t => t.m_name == jsonData.name);

        GameKeywordBase newKeyword;
        switch (m_keywords[i].m_keywordParamType)
        {
            case GameKeywordBase.KeywordParamType.NoParams:
                newKeyword = (GameKeywordBase)Activator.CreateInstance(m_keywords[i].GetType());
                break;
            case GameKeywordBase.KeywordParamType.IntParam:
                newKeyword = (GameKeywordBase)Activator.CreateInstance(m_keywords[i].GetType(), jsonData.intValue);
                break;
            case GameKeywordBase.KeywordParamType.ActionParam:
                JsonActionData jsonActionData = JsonUtility.FromJson<JsonActionData>(jsonData.actionJson);
                newKeyword = (GameKeywordBase)Activator.CreateInstance(m_keywords[i].GetType(), GameActionFactory.GetActionWithName(jsonActionData, gameEntity));
                break;
            default:
                return null;
        }
        newKeyword.LoadFromJson(jsonData);

        return newKeyword;
    }

    public static GameKeywordBase GetKeywordClone(GameKeywordBase other, GameEntity gameEntity)
    {
        return GetKeywordsFromJson(JsonUtility.FromJson<JsonKeywordData>(other.SaveToJsonAsString()), gameEntity);
    }
}
