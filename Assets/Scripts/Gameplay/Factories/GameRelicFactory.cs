using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameRelicFactory
{
    private static List<GameRelic> m_relics = new List<GameRelic>();

    public static void Init(List<GameRelic> relicPool)
    {
        m_relics = relicPool;
    }

    public static GameRelic GetRandomRelic(GameRelic exclusionRelic =  null)
    {
        return GetRandomRelicAtRarity(GetRandomRarity(), exclusionRelic);
    }

    public static GameElementBase.GameRarity GetRandomRarity()
    {
        float random = UnityEngine.Random.Range(0.0f, 100.0f);

        if (random <= Constants.PercentChanceForRareRelic)
        {
            return GameElementBase.GameRarity.Rare;
        }
        else if (random <= Constants.PercentChanceForRareRelic + Constants.PercentChanceForUncommonRelic)
        {
            return GameElementBase.GameRarity.Uncommon;
        }
        else
        {
            return GameElementBase.GameRarity.Common;
        }
    }

    public static GameRelic GetRandomRelicAtRarity(GameElementBase.GameRarity rarity, GameRelic exclusionRelic = null)
    {
        List<GameRelic> relicList = GetListWithoutPlayerRelics();

        for (int i = relicList.Count - 1; i >= 0; i--)
        {
            if (exclusionRelic != null && relicList[i].m_name == exclusionRelic.m_name)
            {
                relicList.RemoveAt(i);
                continue;
            }

            if (relicList[i].m_rarity != rarity)
            {
                relicList.RemoveAt(i);
                continue;
            }
        }

        int r = UnityEngine.Random.Range(0, relicList.Count);

        GameRelic newRelic = (GameRelic)Activator.CreateInstance(relicList[r].GetType());

        return newRelic;
    }

    private static List<GameRelic> GetListWithoutPlayerRelics()
    {
        List<GameRelic> newList = new List<GameRelic>();

        List<GameRelic> playerRelics = GameHelper.GetPlayer().GetRelics().GetRelicListForRead();

        for (int i = 0; i < m_relics.Count; i++)
        {
            bool isDup = false;
            for (int c = 0; c < playerRelics.Count; c++)
            {
                if (m_relics[i].m_name == playerRelics[c].m_name)
                {
                    isDup = true;
                }
            }

            if (!isDup)
            {
                newList.Add(m_relics[i]);
            }
        }

        //If there are no relics left that the player doesn't have, then just allow any relic as a dup
        if (newList.Count == 0)
        {
            return m_relics;
        }

        return newList;
    }
}

