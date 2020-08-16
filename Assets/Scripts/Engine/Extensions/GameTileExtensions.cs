using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Util
{
    public static class GameTileExtensions
    {
        public static List<Vector2Int> AdjacentTiles (this GameTile gridTile)
        {
            List<Vector2Int> adjacentTiles = new List<Vector2Int>();

            Vector2Int tileLeftCoordinates = gridTile.Left();
            if (tileLeftCoordinates != gridTile.m_gridPosition)
                adjacentTiles.Add(tileLeftCoordinates);

            Vector2Int tileRightCoordinates = gridTile.Right();
            if (tileRightCoordinates != gridTile.m_gridPosition)
                adjacentTiles.Add(tileRightCoordinates);

            Vector2Int tileUpLeftCoordinates = gridTile.UpLeft();
            if (tileUpLeftCoordinates != gridTile.m_gridPosition)
                adjacentTiles.Add(tileUpLeftCoordinates);

            Vector2Int tileUpRightCoordinates = gridTile.UpRight();
            if (tileUpRightCoordinates != gridTile.m_gridPosition)
                adjacentTiles.Add(tileUpRightCoordinates);

            Vector2Int tileDownLeftCoordinates = gridTile.DownLeft();
            if (tileDownLeftCoordinates != gridTile.m_gridPosition)
                adjacentTiles.Add(tileDownLeftCoordinates);

            Vector2Int tileDownRightCoordinates = gridTile.DownRight();
            if (tileDownRightCoordinates != gridTile.m_gridPosition)
                adjacentTiles.Add(tileDownRightCoordinates);

            return adjacentTiles;
        }
        
        public static Vector2Int Left(this GameTile gridTile)
        {
            Vector2Int currentPosition = gridTile.m_gridPosition;

            if (currentPosition.x > 0)
            {
                return currentPosition + Vector2Int.left;
            }

            return currentPosition;
        }

        public static Vector2Int Right(this GameTile gridTile)
        {
            Vector2Int currentPosition = gridTile.m_gridPosition;

            if (currentPosition.x < Constants.GridSizeX - 1)
            {
                return currentPosition + Vector2Int.right;
            }

            return currentPosition;
        }

        public static Vector2Int UpLeft(this GameTile gridTile)
        {
            Vector2Int currentPosition = gridTile.m_gridPosition;

            if (!(currentPosition.y == Constants.GridSizeY - 1
                || currentPosition.x == 0 && currentPosition.y % 2 == 0))
            {
                currentPosition += Vector2Int.up;
                if (currentPosition.y % 2 == 1)
                    currentPosition += Vector2Int.left;

            }

            return currentPosition;
        }

        public static Vector2Int UpRight(this GameTile gridTile)
        {
            Vector2Int currentPosition = gridTile.m_gridPosition;

            if (!(currentPosition.y == Constants.GridSizeY - 1
               || currentPosition.x == Constants.GridSizeX - 1 && currentPosition.y % 2 == 1))
            {
                currentPosition += Vector2Int.up;
                if (currentPosition.y % 2 == 0)
                    currentPosition += Vector2Int.right;
            }

            return currentPosition;
        }

        public static Vector2Int DownLeft(this GameTile gridTile)
        {
            Vector2Int currentPosition = gridTile.m_gridPosition;

            if (!(currentPosition.y == 0
                || currentPosition.x == 0 && currentPosition.y % 2 == 0))
            {
                currentPosition += Vector2Int.down;
                if (currentPosition.y % 2 == 1)
                    currentPosition += Vector2Int.left;
            }

            return currentPosition;
        }

        public static Vector2Int DownRight(this GameTile gridTile)
        {
            Vector2Int currentPosition = gridTile.m_gridPosition;

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
