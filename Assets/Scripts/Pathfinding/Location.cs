
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

    public Location(GameTile gameTile, GameTile targetGameTile, int g, Location parent)
    {
        X = gameTile.m_gridPosition.x;
        Y = gameTile.m_gridPosition.y;
        GameTile = gameTile;
        Parent = parent;

        G = g;
        H = CalculateHValue(targetGameTile);
        F = G + H;
    }

    public int CalculateHValue(GameTile targetGameTile)
    {
        Vector2Int currentPosition = new Vector2Int(X, Y);
        Vector2Int targetPosition = targetGameTile.m_gridPosition;
        int distance = 0;

        while (currentPosition.y != targetPosition.y)
        {
            if (currentPosition.y > targetPosition.y)
            {
                if (currentPosition.x >= targetPosition.x)
                    currentPosition = currentPosition.DownLeftCoordinate();
                else
                    currentPosition = currentPosition.DownRightCoordinate();
            }
            else
            {
                if (currentPosition.x >= targetPosition.x)
                    currentPosition = currentPosition.UpLeftCoordinate();
                else
                    currentPosition = currentPosition.UpRightCoordinate();
            }
            distance++;
        }

        return distance + Math.Abs(currentPosition.x - targetPosition.y);
    }

    public Location(GameTile gameTile, int g)
    {
        X = gameTile.m_gridPosition.x;
        Y = gameTile.m_gridPosition.y;
        GameTile = gameTile;

        G = g;
        H = 0;
        F = G + H;
    }

    public Location(GameTile gameTile)
    {
        X = gameTile.m_gridPosition.x;
        Y = gameTile.m_gridPosition.y;
        GameTile = gameTile;
    }
}