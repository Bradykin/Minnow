using Game.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldGridManager : Singleton<WorldGridManager>
{
    public WorldTile[] m_gridArray { get; private set; }

    private bool m_setup;

    void Start()
    {
        
    }

    void Update()
    {
        if (!m_setup)
            return;
    }

    public void Setup(Transform parent)
    {
        Debug.Log("WorldGridManager Setup");
        if (!m_setup)
        {
            SetupSquareGrid(parent);
            GameHelper.MakePlayerBuilding(m_gridArray[Constants.GridSizeX * 3 + 5].GetGameTile(), new ContentCastleBuilding());
            m_setup = true;
        }
    }

    public void RecycleGrid()
    {
        if (m_setup)
        {
            m_setup = false;

            foreach (var tile in m_gridArray)
            {
                Recycler.Recycle<WorldTile>(tile);
            }
        }
    }

    private void SetupSquareGrid(Transform parent)
    {
        int numGridTiles = Constants.GridSizeX * Constants.GridSizeY;
        m_gridArray = new WorldTile[numGridTiles];

        for (int i = numGridTiles-1; i >= 0; i--)
        {
            m_gridArray[i] = FactoryManager.Instance.GetFactory<WorldTileFactory>().CreateObject<WorldTile>();
            m_gridArray[i].transform.parent = parent;

            int x = i % Constants.GridSizeX;
            int y = i / Constants.GridSizeX;

            m_gridArray[i].Init(x, y);
            m_gridArray[i].transform.position = m_gridArray[i].GetScreenPosition();
        }
    }

    public List<GameTile> GetSurroundingTiles(GameTile centerPoint, int outerRange, int innerRange = 1)
    {
        List<WorldTile> worldTiles = GetSurroundingTiles(centerPoint.m_curTile, outerRange, innerRange);

        List<GameTile> returnList = new List<GameTile>();
        
        for (int i = 0; i < worldTiles.Count; i++)
        {
            returnList.Add(worldTiles[i].GetGameTile());
        }

        return returnList;
    }

    //Range 1 = surrounding tiles (but not middle tiles)
    public List<WorldTile> GetSurroundingTiles(WorldTile centerPoint, int outerRange, int innerRange = 1)
    {
        List<WorldTile> returnList = new List<WorldTile>();

        if (innerRange > outerRange)
        {
            Debug.LogError("Invalid input data to GetSurroundingTiles: innerRange > outerRange");
            return returnList;
        }

        for (int i = innerRange; i <= outerRange; i++)
            returnList.AddRange(GetTilesAtRange(centerPoint, i));

        return returnList;
    }

    private List<WorldTile> GetTilesAtRange(WorldTile centerPoint, int range)
    {
        List<WorldTile> returnList = new List<WorldTile>();

        if (range == 0)
        {
            returnList.Add(centerPoint);
            return returnList;
        }

        Vector2Int startingTileCoordinates = centerPoint.GetGameTile().m_gridPosition;
        for (int i = 0; i < range; i++)
            startingTileCoordinates = startingTileCoordinates.LeftCoordinate();

        Vector2Int currentTileCoordinates = startingTileCoordinates;
        WorldTile currentWorldTile;
        for (int i = 0; i < range; i++)
        {
            currentTileCoordinates = currentTileCoordinates.UpRightCoordinate();
            currentWorldTile = GetWorldGridTileAtPosition(currentTileCoordinates);
            if (currentWorldTile != null)
                returnList.Add(currentWorldTile);
        }
        for (int i = 0; i < range; i++)
        {
            currentTileCoordinates = currentTileCoordinates.RightCoordinate();
            currentWorldTile = GetWorldGridTileAtPosition(currentTileCoordinates);
            if (currentWorldTile != null)
                returnList.Add(currentWorldTile);
        }
        for (int i = 0; i < range; i++)
        {
            currentTileCoordinates = currentTileCoordinates.DownRightCoordinate();
            currentWorldTile = GetWorldGridTileAtPosition(currentTileCoordinates);
            if (currentWorldTile != null)
                returnList.Add(currentWorldTile);
        }
        for (int i = 0; i < range; i++)
        {
            currentTileCoordinates = currentTileCoordinates.DownLeftCoordinate();
            currentWorldTile = GetWorldGridTileAtPosition(currentTileCoordinates);
            if (currentWorldTile != null)
                returnList.Add(currentWorldTile);
        }
        for (int i = 0; i < range; i++)
        {
            currentTileCoordinates = currentTileCoordinates.LeftCoordinate();
            currentWorldTile = GetWorldGridTileAtPosition(currentTileCoordinates);
            if (currentWorldTile != null)
                returnList.Add(currentWorldTile);
        }
        for (int i = 0; i < range; i++)
        {
            currentTileCoordinates = currentTileCoordinates.UpLeftCoordinate();
            currentWorldTile = GetWorldGridTileAtPosition(currentTileCoordinates);
            if (currentWorldTile != null)
                returnList.Add(currentWorldTile);
        }

        return returnList;
    }

    public WorldTile GetWorldGridTileAtPosition(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Constants.GridSizeX || y >= Constants.GridSizeY)
            return null;
        
        return m_gridArray[x + y * Constants.GridSizeX];
    }

    public WorldTile GetWorldGridTileAtPosition(Vector2Int position)
    {
        return GetWorldGridTileAtPosition(position.x, position.y);
    }

    //============================================================================================================//

    public void SetupEnemies(GameOpponent gameOpponent)
    {
        StartCoroutine(AddEnemiesToGrid(gameOpponent));
    }    

    private IEnumerator AddEnemiesToGrid(GameOpponent gameOpponent)
    {
        while (!WorldGridManager.Instance.m_setup)
            yield return null;

        for (int i = 0; i < WorldGridManager.Instance.m_gridArray.Length; i++)
        {
            if (GameHelper.PercentChanceRoll(Constants.PercentChanceForTileToContainEnemy))
            {
                GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].GetGameTile();
                GameEnemyEntity enemy = GameEnemyFactory.GetRandomEnemy(gameOpponent);
                gameTile.PlaceEntity(enemy);
                gameOpponent.m_controlledEntities.Add(enemy);
            }
        }
    }

    //============================================================================================================//

    public int GetPathLength(GameTile startingGridTile, GameTile targetGridTile, bool ignoreTerrainDifferences, bool getAdjacentTile)
    {
        List<GameTile> path = CalculateAStarPath(startingGridTile, targetGridTile, ignoreTerrainDifferences, getAdjacentTile);

        if (path == null)
        {
            return Constants.NoPathVal;
        }

        int length = 0;
        for (int i = 1; i < path.Count; i++)
        {
            if (ignoreTerrainDifferences)
                length++;
            else
                length += path[i].GetCostToPass();
        }

        return length;
    }

    public int CalculateAbsoluteDistanceBetweenPositions(GameTile startingGameTile, GameTile targetGameTile)
    {
        Vector2Int currentPosition = startingGameTile.m_gridPosition;
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

    public List<GameTile> CalculateAStarPath(GameTile startingGridTile, GameTile targetGridTile, bool ignoreTerrainDifferences, bool getAdjacentPosition)
    {
        Location current = null;
        var start = new Location(startingGridTile, targetGridTile, 0, null);
        var target = new Location(targetGridTile);
        var openList = new List<Location>();
        var closedList = new List<Location>();

        // start by adding the original position to the open list
        openList.Add(start);

        while (openList.Count > 0)
        {
            // get the square with the lowest F score
            var lowest = openList.Min(l => l.F);
            current = openList.First(l => l.F == lowest);

            // add the current square to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);

            // if we added the destination to the closed list, we've found a path
            if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
            {
                List<GameTile> path = new List<GameTile>();
                while (current != null)
                {
                    path.Add(current.GameTile);
                    current = current.Parent;
                }
                path.Reverse();
                return path;
            }

            List<GameTile> adjacentSquares = current.GameTile.AdjacentTiles();

            foreach (var adjacentTile in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.X == adjacentTile.m_gridPosition.x
                        && l.Y == adjacentTile.m_gridPosition.y) != null)
                    continue;

                if (getAdjacentPosition && adjacentTile.m_gridPosition.x == target.X && adjacentTile.m_gridPosition.y == target.Y)
                {
                    target = current;
                    break;
                }

                if (!ignoreTerrainDifferences && !adjacentTile.IsPassable())
                    continue;

                // if it's not in the open list...
                var adjacent = openList.FirstOrDefault(l => l.X == adjacentTile.m_gridPosition.x
                        && l.Y == adjacentTile.m_gridPosition.y);

                int g;
                if (ignoreTerrainDifferences)
                    g = current.G + 1;
                else
                    g = current.G + adjacentTile.GetCostToPass();

                if (adjacent == null)
                {
                    Location adjacentLocation = new Location(adjacentTile, targetGridTile, g, current);
                    openList.Insert(0, adjacentLocation);
                }
                else if (g < adjacent.G)
                {
                    adjacent.G = g;
                    adjacent.F = adjacent.G + adjacent.H;
                    adjacent.Parent = current;
                }
            }
        }

        Debug.Log("NO VIABLE PATH");
        return null;
    }

    public List<GameTile> GetTilesInMovementRange(GameTile startingGridTile, bool ignoreTerrainDifferences)
    {
        if (!startingGridTile.IsOccupied())
        {
            Debug.Log("NO ENTITY ON TILE");
            return null;
        }
        return GetTilesInMovementRange(startingGridTile, startingGridTile.m_occupyingEntity.GetCurAP(), ignoreTerrainDifferences);
    }

    public List<GameTile> GetTilesInMoveAttackRange(GameTile startingGridTile, bool ignoreTerrainDifferences)
    {
        if (!startingGridTile.IsOccupied())
        {
            Debug.Log("NO ENTITY ON TILE");
            return null;
        }
        int movementAP = startingGridTile.m_occupyingEntity.GetCurAP() - startingGridTile.m_occupyingEntity.GetAPToAttack();

        List<GameTile> tilesInMoveAttackRange = GetTilesInMovementRange(startingGridTile, movementAP, ignoreTerrainDifferences);
        return tilesInMoveAttackRange;
    }

    public List<GameTile> GetTilesInAttackRange(GameTile startingGridTile, bool ignoreTerrainDifferences)
    {
        if (!startingGridTile.IsOccupied())
        {
            Debug.Log("NO ENTITY ON TILE");
            return null;
        }
        int movementAP = startingGridTile.m_occupyingEntity.GetCurAP() - startingGridTile.m_occupyingEntity.GetAPToAttack();
        int range = startingGridTile.m_occupyingEntity.GetRange();

        List<GameTile> tilesInMovementRange = GetTilesInMovementRange(startingGridTile, movementAP, ignoreTerrainDifferences);

        List<GameTile> tilesInAttackRange = new List<GameTile>();
        tilesInAttackRange.AddRange(tilesInMovementRange);

        for (int i = 0; i < tilesInMovementRange.Count; i++)
        {
            List<WorldTile> tiles = GetSurroundingTiles(GetWorldGridTileAtPosition(tilesInMovementRange[i].m_gridPosition), range);
            for (int k = 0; k < tiles.Count; k++)
            {
                if (!tilesInAttackRange.Contains(tiles[k].GetGameTile()))
                {
                    tilesInAttackRange.Add(tiles[k].GetGameTile());
                }
            }
        }

        return tilesInAttackRange;
    }

    private List<GameTile> GetTilesInMovementRange(GameTile startingGridTile, int currentAP, bool ignoreTerrainDifferences)
    {
        Location current = null;
        var start = new Location(startingGridTile, 0);
        var openList = new List<Location>();
        var closedList = new List<Location>();

        // start by adding the original position to the open list
        openList.Add(start);

        while (openList.Count > 0)
        {
            // get the square with the lowest F score
            var lowest = openList.Min(l => l.F);
            current = openList.First(l => l.F == lowest);

            // add the current square to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);

            // if we added the destination to the closed list, we've found a path
            if (current.F >= currentAP)
                continue;

            List<GameTile> adjacentSquares = current.GameTile.AdjacentTiles();

            foreach (var adjacentTile in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.X == adjacentTile.m_gridPosition.x
                        && l.Y == adjacentTile.m_gridPosition.y) != null)
                    continue;

                if (ignoreTerrainDifferences || !adjacentTile.IsPassable())
                    continue;

                // if it's not in the open list...
                var adjacent = openList.FirstOrDefault(l => l.X == adjacentTile.m_gridPosition.x
                        && l.Y == adjacentTile.m_gridPosition.y);

                int g;
                if (ignoreTerrainDifferences)
                    g = current.G + 1;
                else
                    g = current.G + adjacentTile.GetCostToPass();

                if (g > currentAP)
                    continue;

                if (adjacent == null)
                {
                    Location adjacentLocation = new Location(adjacentTile, g);
                    openList.Insert(0, adjacentLocation);
                }
                else if (g < adjacent.G)
                {
                    adjacent.G = g;
                    adjacent.F = adjacent.G + adjacent.H;
                    adjacent.Parent = current;
                }
            }
        }

        List<Location> inRangeLocations = closedList.FindAll(l => l.G <= currentAP);
        List<GameTile> inRangeGameTiles = new List<GameTile>();

        for (int i = 0; i < inRangeLocations.Count; i++)
        {
            inRangeGameTiles.Add(GetWorldGridTileAtPosition(inRangeLocations[i].X, inRangeLocations[i].Y).GetGameTile());
        }

        return inRangeGameTiles;
    }

    public void ClearAllTilesMovementRange()
    {
        for (int i = 0; i < m_gridArray.Length; i++)
        {
            m_gridArray[i].SetMoveable(false);
        }
    }
}
