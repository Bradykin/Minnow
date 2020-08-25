
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
    public GameTile GameTile;

    public Location(GameTile gameTile, GameTile targetGameTile, int g, Location parent)
    {
        X = gameTile.m_gridPosition.x;
        Y = gameTile.m_gridPosition.y;
        GameTile = gameTile;
        Parent = parent;

        G = g;
        H = Math.Abs(targetGameTile.m_gridPosition.x - X) + Math.Abs(targetGameTile.m_gridPosition.y - Y);
        F = G + H;
    }

    public Location(GameTile gameTile)
    {
        X = gameTile.m_gridPosition.x;
        Y = gameTile.m_gridPosition.y;
        GameTile = gameTile;
    }
}