using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    //Game Data
    public static int InitialHandSize = 4;
    public static int StartingEnergy = 3;
    public static int StartingActions = 3;

    public static int PercentChanceForUncommonCard = 24;
    public static int PercentChanceForRareCard = 12;

    public static int MaxChaos = 10;

    //UI Data
    public static float TooltipWidth = 5.5f;

    //Error Handling
    public static int NoPathVal = 99999;

    //Sizing of hexagons
    public static float HexagonInnerRadius = 1.5022f;
    public static float HexagonOuterRadius = HexagonInnerRadius / 0.866025404f;

    //Wave Data
    public static int InitialWaveSize = 6;
    public static int WaveTurnIncrement = 2;
    public static int FinalWaveNum = 8;

    //Testing Data
    public static bool FogOfWar = false;
}
