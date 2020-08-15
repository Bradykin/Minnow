using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Util
{
    public static class GridTileExtensions
    {
        public static Vector2Int? Left(this WorldGridTile gridTile)
        {
            Vector2Int currentPosition = gridTile.GridPosition;

            if (currentPosition.x > 0)
            {
                return currentPosition + Vector2Int.left;
            }

            return null;
        }

        public static Vector2Int? Right(this WorldGridTile gridTile)
        {
            Vector2Int currentPosition = gridTile.GridPosition;

            if (currentPosition.x < Constants.GridSizeX - 1)
            {
                return currentPosition + Vector2Int.right;
            }

            return null;
        }
    }
}
