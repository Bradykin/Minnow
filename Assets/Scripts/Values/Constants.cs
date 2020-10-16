using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Constants
{
    //Game Data
    public static int InitialHandSize = 4;
    public static int StartingEnergy = 3;
    public static int StartingActions = 4;
    public static int MaxHandSize = 10;
    public static int MaxTotalStamina = 12;
    public static int SpawnEliteWave = 2;

    public static int PercentChanceForUncommonCard = 38;
    public static int PercentChanceForRareCard = 18;

    public static int PercentChanceForUncommonRelic = 30;
    public static int PercentChanceForRareRelic = 15;

    public static int PercentChanceForUncommonEvent = 30;
    public static int PercentChanceForRareEvent = 15;

    public static int PercentChanceForEliteToSpawn = 20;
    public static int PercentChanceForMobToSpawn = 40;

    public static int MaxChaos = 10;

    public static int RankOneChaosLevel = 5;
    public static int RankTwoChaosLevel = 10;

    //UI Data
    public static float TooltipWidth = 5.5f;

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

    public static int FinalWaveNum = 6;
    public static int GoldPerWave = 25;

    //Testing Data
    public static bool SnapToCastleAtStart = false;
    public static bool FogOfWar = true;
    public static bool DebugEventsVisibleInFog = true;
#if UNITY_EDITOR
    public static bool CheatsOn = true;
    public static bool UseSteppedOutEnemyTurns = true;
#else
    public static bool CheatsOn = false;
    public static bool UseSteppedOutEnemyTurns = true;
#endif
    public static bool SteppedOutEnemyTurnsCameraFollowMovement = true;
    public static int SteppedOutEnemyTurnsCameraFollowThreshold = 3;
    public static bool UseLocationalSpellcraft = true;

    //Terrain Data
    public static int ForestDamageReduction = 1;
    public static int ForestMovementCost = 2;
    public static int BurnedForestDamageReduction = 0;
    public static int BurnedForestMovementCost = 2;
    public static int PlainsDamageReduction = 0;
    public static int PlainsMovementCost = 1;
    public static int HillsDamageReduction = 2;
    public static int HillsMovementCost = 3;
    public static int HillsRangeModifier = 1;
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
    public static int DunesMovementCost = 2;
    public static int DunesDamageTaken = 2;

    //Snow unique tiles
    public static int SnowBankDamageReduction = 0;
    public static int SnowBankMovementCost = 2;
    public static int SnowBankRangeModifier = 0;
    public static int IceDamageReduction = 0;
    public static int IceMovementCost = 1;

    //Tropical unique tiles
    public static int MarshDamageReduction = 0;
    public static int MarshMovementCost = 2;
    public static int MarshStaminaRegenLoss = 0;
    public static int BogDamageReduction = 0;
    public static int BogMovementCost = 2;
    public static int BogStaminaRegenLoss = 2;

    public static int GetWaveLength (int waveNum)
    {
        return WaveLength[waveNum - 1];
    }
}
