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

        m_hasInit = true;
    }

    public static GameRelic GetRandomRelic()
    {
        if (!m_hasInit)
        {
            Init();
        }

        int r = UnityEngine.Random.Range(0, m_relics.Count);

        return (GameRelic)Activator.CreateInstance(m_relics[r].GetType());
    }
}

