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

            Vector2Int tileLeftCoordinates = gameTile.LeftCoordinate();
            if (tileLeftCoordinates != gameTile.m_gridPosition)
                adjacentTiles.Add(tileLeftCoordinates);

            Vector2Int tileRightCoordinates = gameTile.RightCoordinate();
            if (tileRightCoordinates != gameTile.m_gridPosition)
                adjacentTiles.Add(tileRightCoordinates);

            Vector2Int tileUpLeftCoordinates = gameTile.UpLeftCoordinate();
            if (tileUpLeftCoordinates != gameTile.m_gridPosition)
                adjacentTiles.Add(tileUpLeftCoordinates);

            Vector2Int tileUpRightCoordinates = gameTile.UpRightCoordinate();
            if (tileUpRightCoordinates != gameTile.m_gridPosition)
                adjacentTiles.Add(tileUpRightCoordinates);

            Vector2Int tileDownLeftCoordinates = gameTile.DownLeftCoordinate();
            if (tileDownLeftCoordinates != gameTile.m_gridPosition)
                adjacentTiles.Add(tileDownLeftCoordinates);

            Vector2Int tileDownRightCoordinates = gameTile.DownRightCoordinate();
            if (tileDownRightCoordinates != gameTile.m_gridPosition)
                adjacentTiles.Add(tileDownRightCoordinates);

            return adjacentTiles;
        }

        public static Vector2Int RandomAdjacentTile (this GameTile gameTile)
        {
            List<Vector2Int> adjacentTiles = gameTile.AdjacentTiles();
            return adjacentTiles[Random.Range(0, adjacentTiles.Count)];
        }

        //============================================================================================================//

        public static Vector2Int LeftCoordinate(this GameTile gameTile)
        {
            Vector2Int currentPosition = gameTile.m_gridPosition;

            if (currentPosition.x > 0)
            {
                return currentPosition + Vector2Int.left;
            }

            return currentPosition;
        }

        public static WorldTile LeftWorldTile(this GameTile gameTile)
        {
            return WorldGridManager.Instance.GetWorldGridTileAtPosition(gameTile.LeftCoordinate());
        }

        public static GameTile LeftGameTile(this GameTile gameTile)
        {
            return gameTile.LeftWorldTile().m_gameTile;
        }

        public static Vector2Int RightCoordinate(this GameTile gameTile)
        {
            Vector2Int currentPosition = gameTile.m_gridPosition;

            if (currentPosition.x < Constants.GridSizeX - 1)
            {
                return currentPosition + Vector2Int.right;
            }

            return currentPosition;
        }

        public static WorldTile RightWorldTile(this GameTile gameTile)
        {
            return WorldGridManager.Instance.GetWorldGridTileAtPosition(gameTile.RightCoordinate());
        }

        public static GameTile RightGameTile(this GameTile gameTile)
        {
            return gameTile.RightWorldTile().m_gameTile;
        }

        public static Vector2Int UpLeftCoordinate(this GameTile gameTile)
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

        public static WorldTile UpLeftWorldTile(this GameTile gameTile)
        {
            return WorldGridManager.Instance.GetWorldGridTileAtPosition(gameTile.UpLeftCoordinate());
        }

        public static GameTile UpLeftGameTile(this GameTile gameTile)
        {
            return gameTile.UpLeftWorldTile().m_gameTile;
        }

        public static Vector2Int UpRightCoordinate(this GameTile gameTile)
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

        public static WorldTile UpRightWorldTile(this GameTile gameTile)
        {
            return WorldGridManager.Instance.GetWorldGridTileAtPosition(gameTile.UpRightCoordinate());
        }

        public static GameTile UpRightGameTile(this GameTile gameTile)
        {
            return gameTile.UpRightWorldTile().m_gameTile;
        }

        public static Vector2Int DownLeftCoordinate(this GameTile gameTile)
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

        public static WorldTile DownLeftWorldTile(this GameTile gameTile)
        {
            return WorldGridManager.Instance.GetWorldGridTileAtPosition(gameTile.DownLeftCoordinate());
        }

        public static GameTile DownLeftGameTile(this GameTile gameTile)
        {
            return gameTile.DownLeftWorldTile().m_gameTile;
        }

        public static Vector2Int DownRightCoordinate(this GameTile gameTile)
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

        public static WorldTile DownRightWorldTile(this GameTile gameTile)
        {
            return WorldGridManager.Instance.GetWorldGridTileAtPosition(gameTile.DownRightCoordinate());
        }

        public static GameTile DownRightGameTile(this GameTile gameTile)
        {
            return gameTile.DownRightWorldTile().m_gameTile;
        }
    }
}
