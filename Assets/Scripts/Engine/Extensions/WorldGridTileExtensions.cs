using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Util
{
    public static class WorldGridTileExtensions
    {
        public static Vector2Int Left(this WorldGridTile gridTile)
        {
            Vector2Int currentPosition = gridTile.GridPosition;

            if (currentPosition.x > 0)
            {
                return currentPosition + Vector2Int.left;
            }

            return currentPosition;
        }

        public static Vector2Int Right(this WorldGridTile gridTile)
        {
            Vector2Int currentPosition = gridTile.GridPosition;

            if (currentPosition.x < Constants.GridSizeX - 1)
            {
                return currentPosition + Vector2Int.right;
            }

            return currentPosition;
        }

        public static Vector2Int UpLeft(this WorldGridTile gridTile)
        {
            Vector2Int currentPosition = gridTile.GridPosition;

            if (!(currentPosition.y == Constants.GridSizeY - 1
                || currentPosition.x == 0 && currentPosition.y % 2 == 0))
            {
                currentPosition += Vector2Int.up;
                if (currentPosition.y % 2 == 1)
                    currentPosition += Vector2Int.left;

            }

            return currentPosition;
        }

        public static Vector2Int UpRight(this WorldGridTile gridTile)
        {
            Vector2Int currentPosition = gridTile.GridPosition;

            if (!(currentPosition.y == Constants.GridSizeY - 1
               || currentPosition.x == Constants.GridSizeX - 1 && currentPosition.y % 2 == 1))
            {
                currentPosition += Vector2Int.up;
                if (currentPosition.y % 2 == 0)
                    currentPosition += Vector2Int.right;
            }

            return currentPosition;
        }

        public static Vector2Int DownLeft(this WorldGridTile gridTile)
        {
            Vector2Int currentPosition = gridTile.GridPosition;

            if (!(currentPosition.y == 0
                || currentPosition.x == 0 && currentPosition.y % 2 == 0))
            {
                currentPosition += Vector2Int.down;
                if (currentPosition.y % 2 == 1)
                    currentPosition += Vector2Int.left;
            }

            return currentPosition;
        }

        public static Vector2Int DownRight(this WorldGridTile gridTile)
        {
            Vector2Int currentPosition = gridTile.GridPosition;

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
