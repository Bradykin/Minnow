using Newtonsoft.Json;
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
        m_keywords.Add(new GameBleedKeyword(0));
        m_keywords.Add(new GameBrittleKeyword(0));
        m_keywords.Add(new GameCleaveKeyword());
        m_keywords.Add(new GameDamageReductionKeyword(0));
        m_keywords.Add(new GameDamageShieldKeyword(0));
        m_keywords.Add(new GameDeathKeyword(null));
        m_keywords.Add(new GameDuneswalkKeyword());
        m_keywords.Add(new GameEnrageKeyword(null));
        m_keywords.Add(new GameFadeKeyword());
        m_keywords.Add(new GameForestwalkKeyword());
        m_keywords.Add(new GameFlyingKeyword());
        m_keywords.Add(new GameFrostwalkKeyword());
        m_keywords.Add(new GameLavawalkKeyword());
        m_keywords.Add(new GameKnowledgeableKeyword(null));
        m_keywords.Add(new GameMomentumKeyword(null));
        m_keywords.Add(new GameMountainwalkKeyword());
        m_keywords.Add(new GameRangeKeyword(0));
        m_keywords.Add(new GameRegenerateKeyword(0));
        m_keywords.Add(new GameRootedKeyword());
        m_keywords.Add(new GameShivKeyword());
        m_keywords.Add(new GameSpellcraftKeyword(null));
        m_keywords.Add(new GameSummonKeyword(null));
        m_keywords.Add(new GameTauntKeyword());
        m_keywords.Add(new GameThornsKeyword(0));
        m_keywords.Add(new GameVictoriousKeyword(null));
        m_keywords.Add(new GameWaterboundKeyword());
        m_keywords.Add(new GameWaterwalkKeyword());

        m_hasInit = true;
    }

    public static GameKeywordBase GetKeywordsFromJson(JsonGameKeywordData jsonData, GameUnit gameUnit)
    {
        if (!m_hasInit)
            Init();

        int i = m_keywords.FindIndex(t => t.GetName() == jsonData.name);

        GameKeywordBase newKeyword;
        switch (m_keywords[i].m_keywordParamType)
        {
            case GameKeywordBase.KeywordParamType.NoParams:
                newKeyword = (GameKeywordBase)Activator.CreateInstance(m_keywords[i].GetType());
                break;
            case GameKeywordBase.KeywordParamType.IntParam:
                newKeyword = (GameKeywordBase)Activator.CreateInstance(m_keywords[i].GetType(), jsonData.intValue);
                break;
            case GameKeywordBase.KeywordParamType.BoolParam:
                newKeyword = (GameKeywordBase)Activator.CreateInstance(m_keywords[i].GetType(), args: jsonData.boolValue);
                break;
            case GameKeywordBase.KeywordParamType.IntBoolParam:
                newKeyword = (GameKeywordBase)Activator.CreateInstance(m_keywords[i].GetType(), jsonData.intValue, jsonData.boolValue);
                break;
            case GameKeywordBase.KeywordParamType.ActionParam:
                JsonGameActionData jsonActionData = jsonData.gameActionData[0];
                newKeyword = (GameKeywordBase)Activator.CreateInstance(m_keywords[i].GetType(), GameActionFactory.GetActionFromJson(jsonActionData, gameUnit));
                if (jsonData.gameActionData.Count > 1)
                {
                    for (int k = 1; k < jsonData.gameActionData.Count; k++)
                    {
                        GameKeywordBase anotherKeyword = (GameKeywordBase)Activator.CreateInstance(m_keywords[i].GetType(), GameActionFactory.GetActionFromJson(jsonData.gameActionData[k], gameUnit));
                        newKeyword.AddKeyword(anotherKeyword);
                    }
                }
                break;
            default:
                return null;
        }
        newKeyword.LoadFromJson(jsonData);
        newKeyword.m_isPermanent = jsonData.isPermanentValue;

        return newKeyword;
    }

    public static GameKeywordBase GetKeywordClone(GameKeywordBase other, GameUnit gameUnit)
    {
        return GetKeywordsFromJson(other.SaveToJson(), gameUnit);
    }
}
