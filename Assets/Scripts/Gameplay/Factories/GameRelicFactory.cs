﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameRelicFactory
{
    private static List<GameRelic> m_relics = new List<GameRelic>();

    private static List<GameRelic> m_rareRelics = new List<GameRelic>();
    private static List<GameRelic> m_uncommonRelics = new List<GameRelic>();
    private static List<GameRelic> m_commonRelics = new List<GameRelic>();

    private static List<GameRelic> m_exclusionRelics = new List<GameRelic>();

    private static bool m_hasInit = false;

    public static void Init(List<GameRelic> exclusionRelics = null)
    {
        m_hasInit = true;
        
        m_relics.Clear();
        m_rareRelics.Clear();
        m_uncommonRelics.Clear();
        m_commonRelics.Clear();
        m_exclusionRelics.Clear();

        if (exclusionRelics != null)
        {
            m_exclusionRelics.AddRange(exclusionRelics);
        }
        
        //General Relics
        m_relics.Add(new ContentMaskOfAgesRelic());
        m_relics.Add(new ContentOrbOfEnergyRelic());
        m_relics.Add(new ContentLoadedChestRelic());
        m_relics.Add(new ContentWolvenFangRelic());
        m_relics.Add(new ContentHoovesOfProductionRelic());
        m_relics.Add(new ContentLivingStoneRelic());
        m_relics.Add(new ContentBestialWrathRelic());
        m_relics.Add(new ContentHourglassOfSpeedRelic());
        m_relics.Add(new ContentMorlemainsSkullRelic());
        m_relics.Add(new ContentMysticRuneRelic());
        m_relics.Add(new ContentOrbOfHealthRelic());
        m_relics.Add(new ContentSecretSoupRelic());
        m_relics.Add(new ContentSoulTrapRelic());
        m_relics.Add(new ContentSpiritCatcherRelic());
        m_relics.Add(new ContentSackOfManyShapesRelic());
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
        m_relics.Add(new ContentSigilOfTheSwordsmanRelic());
        m_relics.Add(new ContentBurningShivsRelic());
        m_relics.Add(new ContentPoisonedShivsRelic());
        m_relics.Add(new ContentCallOfTheSeaRelic());
        m_relics.Add(new ContentMaskOfSpeedRelic());
        m_relics.Add(new ContentSackOfSoulsRelic());
        m_relics.Add(new ContentMarkOfTordrimRelic());
        m_relics.Add(new ContentGoldenKnotRelic());
        m_relics.Add(new ContentNamelessFlaskRelic());
        m_relics.Add(new ContentTheGreatestGiftRelic());
        m_relics.Add(new ContentSporetechRelic());
        m_relics.Add(new ContentTokenOfFriendshipRelic());
        m_relics.Add(new ContentJugOfTordrimRelic());
        m_relics.Add(new ContentAlterOfTordrimRelic());
        m_relics.Add(new ContentInstructionsRelic());
        m_relics.Add(new ContentSymbolOfTheAllianceRelic());
        m_relics.Add(new ContentImpaliumRelic());
        m_relics.Add(new ContentIotalRelic());
        m_relics.Add(new ContentFadingLightRelic());
        m_relics.Add(new ContentBeaconOfSanityRelic());
        m_relics.Add(new ContentPriceOfFreedomRelic());
        m_relics.Add(new ContentHeroicTrophyRelic());
        m_relics.Add(new ContentAncientCoinsRelic());
        m_relics.Add(new ContentCarapaceOfTutuiun());
        m_relics.Add(new ContentTauntingPipeRelic());
        m_relics.Add(new ContentBondOfFamilyRelic());
        m_relics.Add(new ContentAncientRitualRelic());
        m_relics.Add(new ContentTokenOfTheUprisingRelic());
        m_relics.Add(new ContentSecretTiesRelic());
        m_relics.Add(new ContentRelicOfVictoryRelic());
        m_relics.Add(new ContentStarOfDenumainRelic());
        m_relics.Add(new ContentTalonOfTheCruelRelic());
        m_relics.Add(new ContentSachelOfDeceptionRelic());
        m_relics.Add(new ContentDiscountTokenRelic());
        m_relics.Add(new ContentHarvestOfTelumRelic());
        m_relics.Add(new ContentCanvasOfHistoryRelic());
        m_relics.Add(new ContentBeadsOfProphecyRelic());
        m_relics.Add(new ContentTotemOfRevengeRelic());
        m_relics.Add(new ContentVoiceOfTheDefenderRelic());
        m_relics.Add(new ContentMemoryOfTheDefenderRelic());
        m_relics.Add(new ContentTailOfLifeRelic());
        m_relics.Add(new ContentAncientMysteryRelic());
        m_relics.Add(new ContentForbiddenKnowledge());
        m_relics.Add(new ContentHistoryInBloodRelic());
        m_relics.Add(new ContentLastHopeRelic());
        m_relics.Add(new ContentProclamationOfSurrenderRelic());
        m_relics.Add(new ContentThornsOfRayRelic());
        m_relics.Add(new ContentPlagueMaskRelic());
        m_relics.Add(new ContentSecretsOfNatureRelic());
        m_relics.Add(new ContentAdvancedWeaponryRelic());
        m_relics.Add(new ContentBeadofJoyRelic());
        m_relics.Add(new ContentTombOfTheDefenderRelic());
        m_relics.Add(new ContentPrimeRibRelic());
        m_relics.Add(new ContentLifebringerRelic());
        m_relics.Add(new ContentShardOfSorrowRelic());
        m_relics.Add(new ContentChargingRingRelic());
        m_relics.Add(new ContentEyeOfMoragRelic());
        m_relics.Add(new ContentEyeOfTelsimirRelic());
        m_relics.Add(new ContentEyeOfDorosonRelic());
        m_relics.Add(new ContentToolOfTheDeadmanRelic());
        m_relics.Add(new ContentSecretOfTheDeepRelic());
        m_relics.Add(new ContentAngelicFeatherRelic());
        m_relics.Add(new ContentBloodFeatherRelic());
        m_relics.Add(new ContentGoldenFeatherRelic());
        m_relics.Add(new ContentTalonOfTheMeradominRelic());
        m_relics.Add(new ContentAncientEvilRelic());
        m_relics.Add(new ContentEverflowingCanteenRelic());
        m_relics.Add(new ContentHealthFlaskRelic());
        m_relics.Add(new ContentToldiranMiracleRelic());
        m_relics.Add(new ContentNectarOfTheSeaGodRelic());

        //Event Relics
        m_relics.Add(new ContentTraditionalMethodsRelic());
        m_relics.Add(new ContentNewInvestmentsRelic());
        m_relics.Add(new ContentTomeOfDuluhainRelic());
        m_relics.Add(new ContentDominerickRefrainRelic());
        m_relics.Add(new ContentRestorationBrickRelic());

        //Altars
        m_relics.Add(new ContentEyeOfTelloRelic());
        m_relics.Add(new ContentTacticsOfMonRelic());
        m_relics.Add(new ContentGreedOfDorphinRelic());
        m_relics.Add(new ContentMightOfSugoRelic());

        int commonRelics = 0;
        int uncommonRelics = 0;
        int rareRelics = 0;

        for (int i = 0; i < m_relics.Count; i++)
        {
            if (m_exclusionRelics.Any(r => r.GetBaseName() == m_relics[i].GetBaseName()))
            {
                continue;
            }
            
            if (m_relics[i].m_rarity == GameElementBase.GameRarity.Common)
            {
                m_commonRelics.Add(m_relics[i]);
                commonRelics++;
            }
            if (m_relics[i].m_rarity == GameElementBase.GameRarity.Uncommon)
            {
                m_uncommonRelics.Add(m_relics[i]);
                uncommonRelics++;
            }
            if (m_relics[i].m_rarity == GameElementBase.GameRarity.Rare)
            {
                m_rareRelics.Add(m_relics[i]);
                rareRelics++;
            }
        }

        //Debug.Log("Common: " + commonRelics + "\nUncommon: " + uncommonRelics + "\nRare: " + rareRelics);
    }

    public static GameRelic GetRandomRelic(GameRelic exclusionRelic =  null)
    {
        if (!m_hasInit)
        {
            Init();
        }

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
        if (!m_hasInit)
        {
            Init();
        }

        List<GameRelic> relicList = GetListWithoutPlayerRelics();

        for (int i = relicList.Count - 1; i >= 0; i--)
        {
            if (!Constants.UnlockAllContent && !GameMetaprogressionUnlocksDataManager.HasUnlocked(relicList[i]))
            {
                relicList.RemoveAt(i);
                continue;
            }
            
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
            Debug.LogWarning("No viable relics found at rarity: " + rarity + "\nSwapping rarity.");
            
            if (rarity == GameElementBase.GameRarity.Common)
            {
                return GetRandomRelicAtRarity(GameElementBase.GameRarity.Rare, exclusionRelic);
            }
            else if (rarity == GameElementBase.GameRarity.Uncommon)
            {
                return GetRandomRelicAtRarity(GameElementBase.GameRarity.Common, exclusionRelic);
            }
            else if (rarity == GameElementBase.GameRarity.Rare)
            {
                return GetRandomRelicAtRarity(GameElementBase.GameRarity.Uncommon, exclusionRelic);
            }
        }

        List<GameRelic> relicListBeforeTagWeights = new List<GameRelic>();
        relicListBeforeTagWeights.AddRange(relicList);

        //Use the tag weights + randomization to get the correct card here.
        if (Constants.GameDirectorTestPrints && exclusionRelic == null)
        {
            Debug.Log("Checking tag weights for all options on this random relic query.");
        }
        int totalWeight = 0;
        for (int i = 0; i < relicList.Count; i++)
        {
            int tagWeight = GameTagHolder.GetTagValueFor(relicList[i]);
            if (Constants.GameDirectorTestPrints && exclusionRelic == null)
            {
                Debug.Log($"{relicList[i].GetBaseName()} with tag weight {tagWeight}");
            }
            if (tagWeight > 0)
            {
                relicList[i].m_storedTagWeight = tagWeight + totalWeight;
                totalWeight += tagWeight;
                if (Constants.GameDirectorTestPrints && exclusionRelic == null)
                {
                    //Debug.Log($"{relicList[i].GetBaseName()} with total weight {relicList[i].m_storedTagWeight}");
                }
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

    public static IReadOnlyList<GameRelic> GetRelicListAtRarity(GameElementBase.GameRarity gameRarity)
    {
        switch (gameRarity)
        {
            case GameElementBase.GameRarity.Common:
                return m_commonRelics;
            case GameElementBase.GameRarity.Uncommon:
                return m_uncommonRelics;
            case GameElementBase.GameRarity.Rare:
                return m_uncommonRelics;
            default:
                Debug.LogError("Empty Relic List for Rarity");
                return new List<GameRelic>();
        }
    }

    private static List<GameRelic> GetListWithoutPlayerRelics()
    {
        if (!m_hasInit)
        {
            Init();
        }

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

    public static GameRelic GetRelicByName(string name)
    {
        if (!m_hasInit)
        {
            Init();
        }

        for (int i = 0; i < m_relics.Count; i++)
        {
            if (m_relics[i].GetName().ToLower() == name.ToLower())
            {
                return GetGameRelicClone(m_relics[i]);
            }
        }

        return null;
    }

    public static GameRelic GetRelicFromJson(JsonGameRelicData jsonData)
    {
        if (!m_hasInit)
            Init();

        GameRelic gameRelic = GetRelicByName(jsonData.name);

        if (gameRelic == null)
        {
            Debug.LogError("Missing Relic class for " + jsonData.name + " in GameRelicFactory");
            return null;
        }

        gameRelic.LoadFromJson(jsonData);
        return gameRelic;
    }
}

