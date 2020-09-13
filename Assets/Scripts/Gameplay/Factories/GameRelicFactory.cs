using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameRelicFactory
{
    private static List<GameRelic> m_relics = new List<GameRelic>();

    private static bool m_hasInit = false;

    public static void Init()
    {
        m_relics.Add(new ContentDominerickRefrainRelic());
        m_relics.Add(new ContentHourglassOfSpeedRelic());
        m_relics.Add(new ContentMaskOfAgesRelic());
        m_relics.Add(new ContentMorlemainsSkullRelic());
        m_relics.Add(new ContentMysticRuneRelic());
        m_relics.Add(new ContentOrbOfEnergyRelic());
        m_relics.Add(new ContentOrbOfHealthRelic());
        m_relics.Add(new ContentSecretSoupRelic());
        m_relics.Add(new ContentSoulTrapRelic());
        m_relics.Add(new ContentSpiritCatcherRelic());
        m_relics.Add(new ContentWolvenFangRelic());
        m_relics.Add(new ContentSackOfManyShapesRelic());
        m_relics.Add(new ContentHoovesOfProductionRelic());
        m_relics.Add(new ContentEmblemOfTianaRelic());
        m_relics.Add(new ContentDestinyRelic());
        m_relics.Add(new ContentPinnacleOfFearRelic());
        m_relics.Add(new ContentNaturalDaggerRelic());
        m_relics.Add(new ContentLoadedChestRelic());
        m_relics.Add(new ContentLegendaryFragmentRelic());
        m_relics.Add(new ContentTomeOfDuluhainRelic());

        m_hasInit = true;
    }

    public static GameRelic GetRandomRelic()
    {
        if (!m_hasInit)
        {
            Init();
        }

        List<GameRelic> relicList = GetListWithoutPlayerRelics();

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

