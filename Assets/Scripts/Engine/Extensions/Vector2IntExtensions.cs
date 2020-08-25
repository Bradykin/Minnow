using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Util
{
    public static class Vector2IntExtensions
    {
        public static List<Vector2Int> AdjacentCoordinates(this Vector2Int centerCoordinate)
        {
            List<Vector2Int> adjacentTiles = new List<Vector2Int>();

            Vector2Int tileLeftCoordinates = centerCoordinate.LeftCoordinate();
            if (!(tileLeftCoordinates.x < 0 || tileLeftCoordinates.y < 0 || tileLeftCoordinates.x >= Constants.GridSizeX || tileLeftCoordinates.y >= Constants.GridSizeY))
                adjacentTiles.Add(tileLeftCoordinates);

            Vector2Int tileRightCoordinates = centerCoordinate.RightCoordinate();
            if (!(tileLeftCoordinates.x < 0 || tileLeftCoordinates.y < 0 || tileLeftCoordinates.x >= Constants.GridSizeX || tileLeftCoordinates.y >= Constants.GridSizeY))
                adjacentTiles.Add(tileRightCoordinates);

            Vector2Int tileUpLeftCoordinates = centerCoordinate.UpLeftCoordinate();
            if (!(tileLeftCoordinates.x < 0 || tileLeftCoordinates.y < 0 || tileLeftCoordinates.x >= Constants.GridSizeX || tileLeftCoordinates.y >= Constants.GridSizeY))
                adjacentTiles.Add(tileUpLeftCoordinates);

            Vector2Int tileUpRightCoordinates = centerCoordinate.UpRightCoordinate();
            if (!(tileLeftCoordinates.x < 0 || tileLeftCoordinates.y < 0 || tileLeftCoordinates.x >= Constants.GridSizeX || tileLeftCoordinates.y >= Constants.GridSizeY))
                adjacentTiles.Add(tileUpRightCoordinates);

            Vector2Int tileDownLeftCoordinates = centerCoordinate.DownLeftCoordinate();
            if (!(tileLeftCoordinates.x < 0 || tileLeftCoordinates.y < 0 || tileLeftCoordinates.x >= Constants.GridSizeX || tileLeftCoordinates.y >= Constants.GridSizeY))
                adjacentTiles.Add(tileDownLeftCoordinates);

            Vector2Int tileDownRightCoordinates = centerCoordinate.DownRightCoordinate();
            if (!(tileLeftCoordinates.x < 0 || tileLeftCoordinates.y < 0 || tileLeftCoordinates.x >= Constants.GridSizeX || tileLeftCoordinates.y >= Constants.GridSizeY))
                adjacentTiles.Add(tileDownRightCoordinates);

            return adjacentTiles;
        }

        public static Vector2Int RandomAdjacentCoordinate(this Vector2Int gameTile)
        {
            List<Vector2Int> adjacentTiles = gameTile.AdjacentCoordinates();
            return adjacentTiles[Random.Range(0, adjacentTiles.Count)];
        }

        //============================================================================================================//

        public static Vector2Int LeftCoordinate(this Vector2Int currentPosition)
        {
            return currentPosition + Vector2Int.left;
        }

        public static Vector2Int RightCoordinate(this Vector2Int currentPosition)
        {
            return currentPosition + Vector2Int.right;
        }

        public static Vector2Int UpLeftCoordinate(this Vector2Int currentPosition)
        {
            currentPosition += Vector2Int.up;
            if (currentPosition.y % 2 == 1)
                currentPosition += Vector2Int.left;

            return currentPosition;
        }

        public static Vector2Int UpRightCoordinate(this Vector2Int currentPosition)
        {
            currentPosition += Vector2Int.up;
            if (currentPosition.y % 2 == 0)
                currentPosition += Vector2Int.right;

            return currentPosition;
        }

        public static Vector2Int DownLeftCoordinate(this Vector2Int currentPosition)
        {
            currentPosition += Vector2Int.down;
            if (currentPosition.y % 2 == 1)
                currentPosition += Vector2Int.left;

            return currentPosition;
        }

        public static Vector2Int DownRightCoordinate(this Vector2Int currentPosition)
        {
            currentPosition += Vector2Int.down;
            if (currentPosition.y % 2 == 0)
                currentPosition += Vector2Int.right;

            return currentPosition;
        }
    }
}