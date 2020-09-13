
using Game.Util;
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

    public bool inClosedList;

    public Location(GameTile gameTile, GameTile targetGameTile, int g, Location parent)
    {
        X = gameTile.m_gridPosition.x;
        Y = gameTile.m_gridPosition.y;
        GameTile = gameTile;
        Parent = parent;
        inClosedList = false;

        G = g;
        H = WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(gameTile, targetGameTile);
        F = G + H;
    }

    public Location(GameTile gameTile, int g)
    {
        X = gameTile.m_gridPosition.x;
        Y = gameTile.m_gridPosition.y;
        GameTile = gameTile;
        inClosedList = false;

        G = g;
        H = 0;
        F = G + H;
    }

    public Location(GameTile gameTile)
    {
        X = gameTile.m_gridPosition.x;
        Y = gameTile.m_gridPosition.y;
        GameTile = gameTile;
        inClosedList = false;
    }
}