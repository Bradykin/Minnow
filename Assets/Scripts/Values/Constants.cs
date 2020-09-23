using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using UnityEngine;

public static class Constants
{
    //Game Data
    public static int InitialHandSize = 4;
    public static int StartingEnergy = 3;
    public static int StartingActions = 4;
    public static int MaxHandSize = 10;
    public static int SpawnEliteWave = 4;

    public static int PercentChanceForUncommonCard = 38;
    public static int PercentChanceForRareCard = 18;

    public static int PercentChanceForEliteToSpawn = 20;
    public static int PercentChanceForMobToSpawn = 40;

    public static int MaxChaos = 10;

    //UI Data
    public static float TooltipWidth = 5.5f;

    //Error Handling
    public static int NoPathVal = 99999;

    //Sizing of hexagons
    public static float HexagonInnerRadius = 1.5022f;
    public static float HexagonOuterRadius = HexagonInnerRadius / 0.866025404f;

    //Wave Data
    public static int InitialWaveSize = 5;
    public static int WaveTurnIncrement = 1;
    public static int FinalWaveNum = 6;
    public static int GoldPerWave = 25;

    //Testing Data
    public static bool SnapToCastleAtStart = false;
    public static bool FogOfWar = true;
    public static bool UseSmartCameraEnemyTurns = false;
    public static bool DebugEventsVisibleInFog = true;
    public static bool CheatsOn = true;
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
    public static int RuinsDamageReduction = 0;
    public static int RuinsMovementCost = 2;

    //Save related file path data
    public const string REMOTE_DATA_PATH = "RemoteData";
    public const string ADD_TO_BUILD_PATH = "AddToBuild";
    public const string BUILD_DATA_PATH = "BuildData";
    public const string MAP_META_DATA_PATH = "SaveMetaData.txt";
    public const string DEFAULT_GRID_DATA_PATH = "JsonGridData0.txt";


    //private static string DATA_FOLDER = $"{}_Data";
    public static string EDITOR_PATH = Path.Combine(new DirectoryInfo(Application.dataPath).Parent.FullName, REMOTE_DATA_PATH, ADD_TO_BUILD_PATH);
    public static string BUILD_PATH = Path.Combine(Application.productName + "_Data", BUILD_DATA_PATH);
}
