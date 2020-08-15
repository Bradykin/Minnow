using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    //Sizing for a "square" hexagon grid
    public static int GridSizeX = 20;
    public static int GridSizeY = 20;

    //Sizing of hexagons
    public static float HexagonInnerRadius = 1.338f;
    public static float HexagonOuterRadius = HexagonInnerRadius / 0.866025404f;
}
