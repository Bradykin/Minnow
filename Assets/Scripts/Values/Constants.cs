using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    //Game Data
    public static int InitialHandSize = 5;
    public static int StartingEnergy = 3;

    //Map Gen Data
    public static int PercentChanceForTileToContainEvent = 5;
    public static int PercentChanceForTileToContainEnemy = 12;

    //Sizing for a "square" hexagon grid
    public static int GridSizeX = 20;
    public static int GridSizeY = 20;

    //Sizing of hexagons
    public static float HexagonInnerRadius = 1.338f;
    public static float HexagonOuterRadius = HexagonInnerRadius / 0.866025404f;
}
