using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameRelicFactory
{
    private static List<GameRelic> m_relics = new List<GameRelic>();

    public static void Init()
    {
        //Starter Relics
        m_relics.Add(new ContentLivingStoneRelic());
        m_relics.Add(new ContentMaskOfAgesRelic());
        m_relics.Add(new ContentOrbOfEnergyRelic());
        m_relics.Add(new ContentLoadedChestRelic());
        m_relics.Add(new ContentWolvenFangRelic());

        //General Relics
        m_relics.Add(new ContentBestialWrathRelic());
        m_relics.Add(new ContentHourglassOfSpeedRelic());
        m_relics.Add(new ContentMorlemainsSkullRelic());
        m_relics.Add(new ContentMysticRuneRelic());
        m_relics.Add(new ContentOrbOfHealthRelic());
        m_relics.Add(new ContentSecretSoupRelic());
        m_relics.Add(new ContentSoulTrapRelic());
        m_relics.Add(new ContentSpiritCatcherRelic());
        m_relics.Add(new ContentSackOfManyShapesRelic());
        m_relics.Add(new ContentHoovesOfProductionRelic());
        m_relics.Add(new ContentDestinyRelic());
        m_relics.Add(new ContentUrbanTacticsRelic());
        m_relics.Add(new ContentPinnacleOfFearRelic());
        m_relics.Add(new ContentNaturalProtectionRelic());
        m_relics.Add(new ContentLegendaryFragmentRelic());
        m_relics.Add(new ContentCursedAmuletRelic());
        m_relics.Add(new ContentDesignSchematicsRelic());
        m_relics.Add(new ContentMedKitRelic());
        m_relics.Add(new ContentLegacyOfMonstersRelic());
        m_relics.Add(new ContentGrandPactRelic());
        m_relics.Add(new ContentTotemOfTheWolfRelic());
        m_relics.Add(new ContentBurningShivsRelic());
        m_relics.Add(new ContentPoisonedShivsRelic());
        m_relics.Add(new ContentCallOfTheSeaRelic());

        //Event Relics
        m_relics.Add(new ContentTraditionalMethodsRelic());
        m_relics.Add(new ContentNewInvestmentsRelic());
        m_relics.Add(new ContentRestorationBrickRelic());
        m_relics.Add(new ContentTomeOfDuluhainRelic());
        m_relics.Add(new ContentDominerickRefrainRelic());
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

        int curLevel = PlayerDataManager.GetCurLevel();

        for (int i = relicList.Count - 1; i >= 0; i--)
        {
            /*if (!Constants.CheatsOn && relicList[i].GetPlayerUnlockLevel() > curLevel) //TODO: alex - Reimplement this functionality with the new system.
            {
                relicList.RemoveAt(i);
                continue;
            }*/
            
            if (exclusionRelic != null && relicList[i].GetName() == exclusionRelic.GetName())
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

        if (relicList.Count == 0)
        {
            Debug.LogError("No viable relics found");
            return null;
        }

        List<GameRelic> relicListBeforeTagWeights = new List<GameRelic>();
        relicListBeforeTagWeights.AddRange(relicList);

        //Use the tag weights + randomization to get the correct card here.
        int totalWeight = 0;
        for (int i = 0; i < relicList.Count; i++)
        {
            int tagWeight = GameTag.GetTagValueFor(relicList[i]);
            if (tagWeight > 0)
            {
                relicList[i].m_storedTagWeight = tagWeight + totalWeight;
                totalWeight += tagWeight;
            }
            else
            {
                relicList.RemoveAt(i);
                i--;
            }
        }

        int r = UnityEngine.Random.Range(0, totalWeight);

        for (int i = 0; i < relicList.Count; i++)
        {
            if (r <= relicList[i].m_storedTagWeight)
            {
                return (GameRelic)Activator.CreateInstance(relicList[i].GetType());
            }
        }

        Debug.LogError("Failed to find any relic when trying get one (likely caused by tag weighting issues).");
        return relicListBeforeTagWeights[UnityEngine.Random.Range(0, relicListBeforeTagWeights.Count)];
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
                if (m_relics[i].GetName() == playerRelics[c].GetName())
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

    public static GameRelic GetGameRelicClone(GameRelic toClone)
    {
        return (GameRelic)Activator.CreateInstance(toClone.GetType());
    }
}

