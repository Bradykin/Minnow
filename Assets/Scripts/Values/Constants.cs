using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    //Game Data
    public static int InitialHandSize = 5;
    public static int StartingEnergy = 3;

    //UI Data
    public static float TooltipWidth = 5.5f;

    //Error Handling
    public static int NoPathVal = 99999;

    //Map Gen Data
    public static int PercentChanceForTileToContainEvent = 5;
    public static int PercentChanceForTileToContainEnemy = 12;

    public static int PercentChanceForTerrainGrasslands = 64;
    public static int PercentChanceForTerrainWater = 8;
    public static int PercentChanceForTerrainForest = 15;
    public static int PercentChanceForTerrainMountain = 8;
    public static int PercentChanceForTerrainRuins = 5;

    //Sizing for a "square" hexagon grid
    public static int GridSizeX = 20;
    public static int GridSizeY = 20;

    //Sizing of hexagons
    public static float HexagonInnerRadius = 1.338f;
    public static float HexagonOuterRadius = HexagonInnerRadius / 0.866025404f;


    //Testing Data
    public static bool FogOfWar = true;
}
