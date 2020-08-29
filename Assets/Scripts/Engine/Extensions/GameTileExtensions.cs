using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Util
{
    public static class GameTileExtensions
    {
        public static List<GameTile> AdjacentTiles (this GameTile gameTile)
        {
            List<GameTile> adjacentTiles = new List<GameTile>();

            WorldTile worldTileLeft = gameTile.LeftWorldTile();
            if (worldTileLeft != null)
                adjacentTiles.Add(worldTileLeft.GetGameTile());

            WorldTile worldTileRight = gameTile.RightWorldTile();
            if (worldTileRight != null)
                adjacentTiles.Add(worldTileRight.GetGameTile());

            WorldTile worldTileUpLeft = gameTile.UpLeftWorldTile();
            if (worldTileUpLeft != null)
                adjacentTiles.Add(worldTileUpLeft.GetGameTile());

            WorldTile worldTileUpRight = gameTile.UpRightWorldTile();
            if (worldTileUpRight != null)
                adjacentTiles.Add(worldTileUpRight.GetGameTile());

            WorldTile worldTileDownLeft = gameTile.DownLeftWorldTile();
            if (worldTileDownLeft != null)
                adjacentTiles.Add(worldTileDownLeft.GetGameTile());

            WorldTile worldTileDownRight = gameTile.DownRightWorldTile();
            if (worldTileDownRight != null)
                adjacentTiles.Add(worldTileDownRight.GetGameTile());

            return adjacentTiles;
        }

        public static GameTile RandomAdjacentTile (this GameTile gameTile)
        {
            List<GameTile> adjacentTiles = gameTile.AdjacentTiles();
            return adjacentTiles[Random.Range(0, adjacentTiles.Count)];
        }

        //============================================================================================================//

        public static Vector2Int LeftCoordinate(this GameTile gameTile)
        {
            return gameTile.m_gridPosition + Vector2Int.left;
        }

        public static WorldTile LeftWorldTile(this GameTile gameTile)
        {
            return WorldGridManager.Instance.GetWorldGridTileAtPosition(gameTile.LeftCoordinate());
        }

        public static Vector2Int RightCoordinate(this GameTile gameTile)
        {
            return gameTile.m_gridPosition + Vector2Int.right;
        }

        public static WorldTile RightWorldTile(this GameTile gameTile)
        {
            return WorldGridManager.Instance.GetWorldGridTileAtPosition(gameTile.RightCoordinate());
        }

        public static Vector2Int UpLeftCoordinate(this GameTile gameTile)
        {
            Vector2Int currentPosition = gameTile.m_gridPosition;

            currentPosition += Vector2Int.up;
            if (currentPosition.y % 2 == 1)
                currentPosition += Vector2Int.left;

            return currentPosition;
        }

        public static WorldTile UpLeftWorldTile(this GameTile gameTile)
        {
            return WorldGridManager.Instance.GetWorldGridTileAtPosition(gameTile.UpLeftCoordinate());
        }

        public static Vector2Int UpRightCoordinate(this GameTile gameTile)
        {
            Vector2Int currentPosition = gameTile.m_gridPosition;

            currentPosition += Vector2Int.up;
            if (currentPosition.y % 2 == 0)
                currentPosition += Vector2Int.right;

            return currentPosition;
        }

        public static WorldTile UpRightWorldTile(this GameTile gameTile)
        {
            return WorldGridManager.Instance.GetWorldGridTileAtPosition(gameTile.UpRightCoordinate());
        }

        public static Vector2Int DownLeftCoordinate(this GameTile gameTile)
        {
            Vector2Int currentPosition = gameTile.m_gridPosition;

            currentPosition += Vector2Int.down;
            if (currentPosition.y % 2 == 1)
                currentPosition += Vector2Int.left;

            return currentPosition;
        }

        public static WorldTile DownLeftWorldTile(this GameTile gameTile)
        {
            return WorldGridManager.Instance.GetWorldGridTileAtPosition(gameTile.DownLeftCoordinate());
        }

        public static Vector2Int DownRightCoordinate(this GameTile gameTile)
        {
            Vector2Int currentPosition = gameTile.m_gridPosition;

            currentPosition += Vector2Int.down;
            if (currentPosition.y % 2 == 0)
                currentPosition += Vector2Int.right;

            return currentPosition;
        }

        public static WorldTile DownRightWorldTile(this GameTile gameTile)
        {
            return WorldGridManager.Instance.GetWorldGridTileAtPosition(gameTile.DownRightCoordinate());
        }
    }
}
