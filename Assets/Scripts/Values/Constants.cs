using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Constants
{
    //Game Data
    public static int InitialHandSize = 4;
    public static int StartingEnergy = 3;

    public static int StartingActions = 3;
    public static int MaxHandSize = 10;
    public static int MaxTotalStamina = 12;
    public static int SpawnEliteTurn = 2;
    public static int SpawnBossTurn = 2;

    public static int PercentChanceForUncommonCard = 38;
    public static int PercentChanceForRareCard = 18;

    public static int PercentChanceForUncommonRelic = 30;
    public static int PercentChanceForRareRelic = 15;

    public static int PercentChanceForUncommonEvent = 55;
    public static int PercentChanceForRareEvent = 22;

    public static int PercentChanceForMobToSpawn = 40;

    public static int NumCommonChests = 2;
    public static int NumUncommonChests = 1;
    public static int NumRareChests = 1;

    public static int CloseGoldVal = 15;
    public static int FarGoldVal = 30;
    public static int NumCloseGold = 7;
    public static int NumFarGold = 10;

    public static int AltarWave = 4;
    public static int WinterStormDamage = 3;
    public static int WinterStormVisionRange = 1;

    public static int IntermissionBuffValue = 1;

    public static int MaxChaos = 5;

    //Sizing for a "square" hexagon grid
    public static int GridSizeX = 30;
    public static int GridSizeY = 30;

    //UI Data
    public static float TooltipWidth = 120.0f;

    //Error Handling
    public static int NoPathVal = 99999;

    //Sizing of hexagons
    public static float HexagonInnerRadius = 1.5022f;
    public static float HexagonOuterRadius = HexagonInnerRadius / 0.866025404f;

    //Wave Data
    //public static int InitialWaveSize = 5;
    //public static int WaveTurnIncrement = 1;
    private static List<int> WaveLength = new List<int>
    {
        7,
        7,
        8,
        8,
        9,
        9
    };

    private static List<int> WaveKillCount = new List<int>
    {
        8,
        12,
        16,
        20,
        25,
        30
    };

    public static int FinalWaveNum = 6;
    public static int GoldPerWave = 0;

    //Testing Data
    public static bool SnapToCastleAtStart = false;
    public static bool FogOfWar = true;
    public static bool DebugEventsVisibleInFog = true;
#if UNITY_EDITOR
    public static bool GameDirectorTestPrints = false;
    public static bool DevMode = true;
    public static bool DebugSeeAllThroughFog = false;
    public static bool UnlockAllContent = false;
#else
    public static bool GameDirectorTestPrints = false;
    public static bool DevMode = false;
    public static bool DebugSeeAllThroughFog = false;
    public static bool UnlockAllContent = false;
#endif
    public static bool SteppedOutEnemyTurnsCameraFollowMovement = true;
    public static int SteppedOutEnemyTurnsCameraFollowThreshold = 3;
    public static bool UseLocationalSpellcraft = true;

    public static float CoverProtectionPercent = 50.0f;


    public static int HillsRangeModifier = 1;
    public static int LavaFieldDamageDealt = 10;
    public static int SandDuneMagicDamageReductionPercentage = 50;

    //Terrain Data
    /*public static int ForestDamageReduction = 1;
    public static int ForestMovementCost = 2;
    public static int BurnedForestDamageReduction = 0;
    public static int BurnedForestMovementCost = 2;
    public static int PlainsDamageReduction = 0;
    public static int PlainsMovementCost = 1;
    public static int HillsDamageReduction = 2;
    public static int HillsMovementCost = 3;
    public static int MountainsDamageReduction = 4;
    public static int MountainsMovementCost = 2;
    public static int WaterDamageReduction = 0;
    public static int WaterMovementCost = 2;
    public static int RuinsDamageReduction = 0;
    public static int RuinsMovementCost = 2;

    //Desert unique tiles
    public static int HalfDunesDamageReduction = 0;
    public static int HalfDunesMovementCost = 1;
    public static int HalfDunesDamageTaken = 1;
    public static int DunesDamageReduction = 0;
    public static int DunesMovementCost = 3;
    public static int DunesDamageTaken = 0;

    //Volcano unique tiles
    public static int LavaFieldDamageReduction = 0;
    public static int LavaFieldMovementCost = 2;

    //Snow unique tiles
    public static int SnowBankDamageReduction = 0;
    public static int SnowBankMovementCost = 2;
    public static int SnowBankRangeModifier = 0;
    public static int IceDamageReduction = 0;
    public static int IceMovementCost = 1;

    //Tropical unique tiles
    public static int MarshDamageReduction = 0;
    public static int MarshMovementCost = 2;
    public static int BogDamageReduction = 0;
    public static int BogMovementCost = 3;
    public static int BogStaminaRegenLoss = 0;*/

    public static int GetWaveLength (int waveNum)
    {
        if (waveNum == 6)
        {
            return 9999;
        }
        
        return WaveLength[waveNum - 1];
    }

    public static int GetWaveKillCount(int waveNum)
    {
        if (waveNum == 6)
        {
            return 9999;
        }

        return WaveKillCount[waveNum - 1];
    }
}
