
using System;
using UnityEngine;

public class Location
{
    public int X;
    public int Y;
    public int F;
    public int G;
    public int H;
    public Location Parent;
    public WorldGridTile GridTile;

    public Location(WorldGridTile gridTile, WorldGridTile targetGridTile, int g, Location parent)
    {
        X = gridTile.GridPosition.x;
        Y = gridTile.GridPosition.y;
        GridTile = gridTile;
        Parent = parent;

        G = g;
        H = Math.Abs(targetGridTile.GridPosition.x - X) + Math.Abs(targetGridTile.GridPosition.y - Y);
        F = G + H;
    }

    public Location(WorldGridTile gridTile)
    {
        X = gridTile.GridPosition.x;
        Y = gridTile.GridPosition.y;
        GridTile = gridTile;
    }
}