
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
    public GameTile GridTile;

    public Location(GameTile gridTile, GameTile targetGridTile, int g, Location parent)
    {
        X = gridTile.m_gridPosition.x;
        Y = gridTile.m_gridPosition.y;
        GridTile = gridTile;
        Parent = parent;

        G = g;
        H = Math.Abs(targetGridTile.m_gridPosition.x - X) + Math.Abs(targetGridTile.m_gridPosition.y - Y);
        F = G + H;
    }

    public Location(GameTile gridTile)
    {
        X = gridTile.m_gridPosition.x;
        Y = gridTile.m_gridPosition.y;
        GridTile = gridTile;
    }
}