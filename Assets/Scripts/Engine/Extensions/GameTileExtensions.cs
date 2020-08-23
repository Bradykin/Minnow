using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Util
{
    public static class GameTileExtensions
    {
        public static List<Vector2Int> AdjacentTiles (this GameTile gameTile)
        {
            List<Vector2Int> adjacentTiles = new List<Vector2Int>();

            Vector2Int tileLeftCoordinates = gameTile.Left();
            if (tileLeftCoordinates != gameTile.m_gridPosition)
                adjacentTiles.Add(tileLeftCoordinates);

            Vector2Int tileRightCoordinates = gameTile.Right();
            if (tileRightCoordinates != gameTile.m_gridPosition)
                adjacentTiles.Add(tileRightCoordinates);

            Vector2Int tileUpLeftCoordinates = gameTile.UpLeft();
            if (tileUpLeftCoordinates != gameTile.m_gridPosition)
                adjacentTiles.Add(tileUpLeftCoordinates);

            Vector2Int tileUpRightCoordinates = gameTile.UpRight();
            if (tileUpRightCoordinates != gameTile.m_gridPosition)
                adjacentTiles.Add(tileUpRightCoordinates);

            Vector2Int tileDownLeftCoordinates = gameTile.DownLeft();
            if (tileDownLeftCoordinates != gameTile.m_gridPosition)
                adjacentTiles.Add(tileDownLeftCoordinates);

            Vector2Int tileDownRightCoordinates = gameTile.DownRight();
            if (tileDownRightCoordinates != gameTile.m_gridPosition)
                adjacentTiles.Add(tileDownRightCoordinates);

            return adjacentTiles;
        }

        public static Vector2Int RandomAdjacentTile (this GameTile gameTile)
        {
            List<Vector2Int> adjacentTiles = gameTile.AdjacentTiles();
            return adjacentTiles[Random.Range(0, adjacentTiles.Count)];
        }
        
        public static Vector2Int Left(this GameTile gameTile)
        {
            Vector2Int currentPosition = gameTile.m_gridPosition;

            if (currentPosition.x > 0)
            {
                return currentPosition + Vector2Int.left;
            }

            return currentPosition;
        }

        public static Vector2Int Right(this GameTile gameTile)
        {
            Vector2Int currentPosition = gameTile.m_gridPosition;

            if (currentPosition.x < Constants.GridSizeX - 1)
            {
                return currentPosition + Vector2Int.right;
            }

            return currentPosition;
        }

        public static Vector2Int UpLeft(this GameTile gameTile)
        {
            Vector2Int currentPosition = gameTile.m_gridPosition;

            if (!(currentPosition.y == Constants.GridSizeY - 1
                || currentPosition.x == 0 && currentPosition.y % 2 == 0))
            {
                currentPosition += Vector2Int.up;
                if (currentPosition.y % 2 == 1)
                    currentPosition += Vector2Int.left;

            }

            return currentPosition;
        }

        public static Vector2Int UpRight(this GameTile gameTile)
        {
            Vector2Int currentPosition = gameTile.m_gridPosition;

            if (!(currentPosition.y == Constants.GridSizeY - 1
               || currentPosition.x == Constants.GridSizeX - 1 && currentPosition.y % 2 == 1))
            {
                currentPosition += Vector2Int.up;
                if (currentPosition.y % 2 == 0)
                    currentPosition += Vector2Int.right;
            }

            return currentPosition;
        }

        public static Vector2Int DownLeft(this GameTile gameTile)
        {
            Vector2Int currentPosition = gameTile.m_gridPosition;

            if (!(currentPosition.y == 0
                || currentPosition.x == 0 && currentPosition.y % 2 == 0))
            {
                currentPosition += Vector2Int.down;
                if (currentPosition.y % 2 == 1)
                    currentPosition += Vector2Int.left;
            }

            return currentPosition;
        }

        public static Vector2Int DownRight(this GameTile gameTile)
        {
            Vector2Int currentPosition = gameTile.m_gridPosition;

            if (!(currentPosition.y == 0
               || currentPosition.x == Constants.GridSizeX - 1 && currentPosition.y % 2 == 1))
            {
                currentPosition += Vector2Int.down;
                if (currentPosition.y % 2 == 0)
                    currentPosition += Vector2Int.right;
            }

            return currentPosition;
        }
    }
}
