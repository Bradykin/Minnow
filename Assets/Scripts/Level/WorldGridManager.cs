using Game.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGridManager : Singleton<WorldGridManager>, ISave, ILoad<JsonGridData>
{
    public WorldTile[] m_gridArray { get; private set; }

    private JsonGridData? m_jsonGridData = null;

    private int m_gridSizeX;
    private int m_gridSizeY;

    private bool m_setup;

    private Transform m_gridRoot;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (!m_setup)
            return;
    }

    public bool IsSetup()
    {
        return m_setup;
    }

    public void Setup(Transform parent)
    {
        if (m_setup)
        {
            return;
        }

        if (m_jsonGridData.HasValue)
        {
            JsonGridData jsonGridData = m_jsonGridData.Value;
            SetupEmptyGrid(parent, jsonGridData.gridSizeX, jsonGridData.gridSizeY);
            LoadJsonGrid(jsonGridData);
        }
        else
        {
            SetupEmptyGrid(parent, Globals.GridSizeX, Globals.GridSizeY);
        }

        m_setup = true;
    }

    public void SetupEmptyGrid(Transform parent, int gridSizeX, int gridSizeY)
    {
        if (m_setup)
        {
            RecycleGrid();
        }

        SetupSquareGrid(parent, gridSizeX, gridSizeY);
        m_setup = true;
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

    private void SetupSquareGrid(Transform parent, int gridSizeX, int gridSizeY)
    {
        m_gridRoot = parent;
        m_gridSizeX = gridSizeX;
        m_gridSizeY = gridSizeY;
        int numGridTiles = m_gridSizeX * m_gridSizeY;
        m_gridArray = new WorldTile[numGridTiles];

        for (int i = numGridTiles-1; i >= 0; i--)
        {
            m_gridArray[i] = FactoryManager.Instance.GetFactory<WorldTileFactory>().CreateObject<WorldTile>();
            m_gridArray[i].transform.parent = m_gridRoot;

            int x = i % Globals.GridSizeX;
            int y = i / Globals.GridSizeX;

            m_gridArray[i].Init(x, y);
            m_gridArray[i].transform.position = m_gridArray[i].GetScreenPosition();
        }
    }

    private void LoadJsonGrid(JsonGridData jsonGridData)
    {
        for (int i = 0; i < jsonGridData.jsonTileData.Count; i++)
        {
            JsonGameTileData jsonGameTileData = JsonUtility.FromJson<JsonGameTileData>(jsonGridData.jsonTileData[i]);
            GetWorldGridTileAtPosition(jsonGameTileData.gridPosition).GetGameTile().LoadFromJson(jsonGameTileData);
        }
    }

    public List<GameTile> GetSurroundingTiles(GameTile centerPoint, int outerRange, int innerRange = 1)
    {
        List<WorldTile> worldTiles = GetSurroundingTiles(centerPoint.GetWorldTile(), outerRange, innerRange);

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

        if (centerPoint == null || centerPoint.GetGameTile() == null || centerPoint.GetGameTile().m_gridPosition == null)
        {
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
        if (x < 0 || y < 0 || x >= Globals.GridSizeX || y >= Globals.GridSizeY)
            return null;
        
        return m_gridArray[x + y * Globals.GridSizeX];
    }

    public WorldTile GetWorldGridTileAtPosition(Vector2Int position)
    {
        return GetWorldGridTileAtPosition(position.x, position.y);
    }

    //============================================================================================================//

    public void SetupWorldGridTeams(GamePlayer gamePlayer, GameOpponent gameOpponent)
    {
        StartCoroutine(SetupWorldGridTeamsIEnumerator(gamePlayer, gameOpponent));
    }

    private IEnumerator SetupWorldGridTeamsIEnumerator(GamePlayer gamePlayer, GameOpponent gameOpponent)
    {
        while (!WorldGridManager.Instance.m_setup)
            yield return null;

        for (int i = 0; i < m_gridArray.Length; i++)
        {
            GameTile curTile = m_gridArray[i].GetGameTile();
            if (curTile.m_occupyingEntity != null)
            {
                if (curTile.m_occupyingEntity.GetTeam() == Team.Player)
                {
                    gamePlayer.AddControlledEntity(curTile.m_occupyingEntity);
                }
                else if (curTile.m_occupyingEntity.GetTeam() == Team.Enemy && curTile.m_occupyingEntity is GameEnemyEntity gameEnemyEntity)
                {
                    gameOpponent.AddControlledEntity(gameEnemyEntity);
                }
                else
                {
                    Debug.LogError("Problem loading entities from world grid - did not match previous criteria");
                }
            }
            if (curTile.GetBuilding() != null)
            {
                gamePlayer.AddControlledBuilding(curTile.GetBuilding());
            }
            if (curTile.m_spawnPoint != null)
            {
                gameOpponent.m_spawnPoints.Add(curTile.m_spawnPoint);
            }
        }

        UICameraController.Instance.SnapToGameObject(WorldController.Instance.m_gameController.m_player.Castle.GetWorldTile().gameObject);
    }

    /*public void SetupEnemies(GameOpponent gameOpponent)
    {
        StartCoroutine(AddEnemiesToGrid(gameOpponent));
    }    

    private IEnumerator AddEnemiesToGrid(GameOpponent gameOpponent)
    {
        while (!WorldGridManager.Instance.m_setup)
            yield return null;

        WorldController.Instance.StartWaveEnemySpawn();
    }*/

    //============================================================================================================//

    public int GetPathLength(GameTile startingGridTile, GameTile targetGridTile, bool ignoreTerrainDifferences, bool getAdjacentTile, bool letPassEnemies)
    {
        List<GameTile> path = CalculateAStarPath(startingGridTile, targetGridTile, ignoreTerrainDifferences, getAdjacentTile, letPassEnemies);

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
                length += path[i].GetCostToPass(startingGridTile.m_occupyingEntity);
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

    public List<GameTile> CalculatePathTowards(GameTile startingGridTile, GameTile targetGridTile, bool ignoreTerrainDifferences, bool getAdjacentPosition, int curAP)
    {
        Location current = null;
        var start = new Location(startingGridTile, targetGridTile, 0, null);
        var target = new Location(targetGridTile);
        var openList = new List<Location>();
        var closedList = new List<Location>();

        var adjacentTiles = new List<GameTile>();
        if (getAdjacentPosition)
        {
            adjacentTiles = targetGridTile.AdjacentTiles();
        }

        // start by adding the original position to the open list
        openList.Add(start);

        while (openList.Count > 0)
        {
            // get the tile with the lowest F score
            current = GetNextTileFromOpenList(openList);

            // add the current tile to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);

            // if the current position is the target position, we have a path
            if (current.X == target.X && current.Y == target.Y
                || (getAdjacentPosition && adjacentTiles.Find(t => t.m_gridPosition.x == current.X && t.m_gridPosition.y == current.Y) != null))
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
                if (closedList.Find(l => l.X == adjacentTile.m_gridPosition.x
                        && l.Y == adjacentTile.m_gridPosition.y) != null)
                    continue;

                if (!ignoreTerrainDifferences && !adjacentTile.IsPassable(startingGridTile.m_occupyingEntity, current.F >= curAP))
                    continue;

                // if it's not in the open list...
                var adjacent = openList.Find(l => l.X == adjacentTile.m_gridPosition.x
                        && l.Y == adjacentTile.m_gridPosition.y);

                int g;
                if (ignoreTerrainDifferences)
                    g = current.G + 1;
                else
                    g = current.G + adjacentTile.GetCostToPass(startingGridTile.m_occupyingEntity);

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

        return null;
    }

    public List<GameTile> CalculateAStarPath(GameTile startingGridTile, GameTile targetGridTile, bool ignoreTerrainDifferences, bool getAdjacentPosition, bool letPassEnemies)
    {
        Location current = null;
        var start = new Location(startingGridTile, targetGridTile, 0, null);
        var target = new Location(targetGridTile);
        var openList = new List<Location>();
        var closedList = new List<Location>();

        var adjacentTiles = new List<GameTile>();
        if (getAdjacentPosition)
        {
            adjacentTiles = targetGridTile.AdjacentTiles();
        }

        // start by adding the original position to the open list
        openList.Add(start);

        while (openList.Count > 0)
        {
            // get the tile with the lowest F score
            current = GetNextTileFromOpenList(openList);

            // add the current tile to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);

            // if the current position is the target position, we have a path
            if (current.X == target.X && current.Y == target.Y
                || (getAdjacentPosition && adjacentTiles.Find(t => t.m_gridPosition.x == current.X && t.m_gridPosition.y == current.Y) != null))
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
                if (closedList.Find(l => l.X == adjacentTile.m_gridPosition.x
                        && l.Y == adjacentTile.m_gridPosition.y) != null)
                    continue;

                if (!ignoreTerrainDifferences && !adjacentTile.IsPassable(startingGridTile.m_occupyingEntity, letPassEnemies))
                    continue;

                // if it's not in the open list...
                var adjacent = openList.Find(l => l.X == adjacentTile.m_gridPosition.x
                        && l.Y == adjacentTile.m_gridPosition.y);

                int g;
                if (ignoreTerrainDifferences)
                    g = current.G + 1;
                else
                    g = current.G + adjacentTile.GetCostToPass(startingGridTile.m_occupyingEntity);

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

        /*string targetAt = "";
        if (targetGridTile.m_occupyingEntity != null)
        {
            targetAt = targetGridTile.m_occupyingEntity.m_name;
        }
        else if (targetGridTile.GetBuilding() != null)
        {
            targetAt = targetGridTile.GetBuilding().m_name;
        }
        else
        {
            targetAt = "NoTarget";
        }

        print("NO VALID PATHS FOR " + startingGridTile.m_occupyingEntity + " AT " + startingGridTile.m_gridPosition + " TO " + targetAt + " AT " + targetGridTile.m_gridPosition);*/

        return null;
    }

    private Location GetNextTileFromOpenList(List<Location> openList)
    {
        if (openList.Count == 0)
        {
            return null;
        }

        int indexToReturn = 0;
        int curF = openList[indexToReturn].F;
        int curH = openList[indexToReturn].H;
        for (int i = 1; i < openList.Count; i++)
        {
            if (openList[i].F < curF || (openList[i].F == curF && openList[i].H < curH))
            {
                indexToReturn = i;
                curF = openList[indexToReturn].F;
                curH = openList[indexToReturn].H;
            }
        }

        return openList[indexToReturn];
    }

    public List<GameTile> GetTilesInMovementRange(GameTile startingGridTile, bool ignoreTerrainDifferences, bool letPassEnemies)
    {
        if (startingGridTile == null || !startingGridTile.IsOccupied() || startingGridTile.m_occupyingEntity.m_isDead)
        {
            Debug.Log("NO ENTITY ON TILE");
            return new List<GameTile>();
        }
        return GetTilesInMovementRange(startingGridTile, startingGridTile.m_occupyingEntity.GetCurAP(), ignoreTerrainDifferences, letPassEnemies);
    }

    public List<GameTile> GetTilesInMovementRangeWithAPToAttack(GameTile startingGridTile, bool ignoreTerrainDifferences, bool letPassEnemies)
    {
        if (startingGridTile == null || !startingGridTile.IsOccupied() || startingGridTile.m_occupyingEntity.m_isDead)
        {
            Debug.Log("NO ENTITY ON TILE");
            return null;
        }
        int movementAP = startingGridTile.m_occupyingEntity.GetCurAP() - startingGridTile.m_occupyingEntity.GetAPToAttack();

        List<GameTile> tilesInMovementRangeWithAPToAttack = GetTilesInMovementRange(startingGridTile, movementAP, ignoreTerrainDifferences, letPassEnemies);
        return tilesInMovementRangeWithAPToAttack;
    }

    public List<GameTile> GetTilesInRangeToMoveAndAttack(GameTile startingGridTile, bool ignoreTerrainDifferences, bool letPassEnemies)
    {
        if (startingGridTile == null || !startingGridTile.IsOccupied() || startingGridTile.m_occupyingEntity.m_isDead)
        {
            Debug.Log("NO ENTITY ON TILE");
            return null;
        }

        List<GameTile> tilesInMovementRangeWithAPToAttack = GetTilesInMovementRangeWithAPToAttack(startingGridTile, ignoreTerrainDifferences, letPassEnemies);
        for (int i = tilesInMovementRangeWithAPToAttack.Count - 1; i >= 0; i--)
        {
            if (tilesInMovementRangeWithAPToAttack[i].IsOccupied() && !tilesInMovementRangeWithAPToAttack[i].m_occupyingEntity.m_isDead && tilesInMovementRangeWithAPToAttack[i] != startingGridTile)
            {
                tilesInMovementRangeWithAPToAttack.RemoveAt(i);
            }
        }

        int range = startingGridTile.m_occupyingEntity.GetRange();

        List<GameTile> tilesInRangeToMoveAndAttack = new List<GameTile>();

        for (int i = 0; i < tilesInMovementRangeWithAPToAttack.Count; i++)
        {
            if (tilesInMovementRangeWithAPToAttack[i].IsOccupied() && !tilesInMovementRangeWithAPToAttack[i].m_occupyingEntity.m_isDead && tilesInMovementRangeWithAPToAttack[i] != startingGridTile)
            {
                continue;
            }
            
            List<WorldTile> tiles = GetSurroundingTiles(GetWorldGridTileAtPosition(tilesInMovementRangeWithAPToAttack[i].m_gridPosition), range);
            for (int k = 0; k < tiles.Count; k++)
            {
                if (!tilesInRangeToMoveAndAttack.Contains(tiles[k].GetGameTile()))
                {
                    tilesInRangeToMoveAndAttack.Add(tiles[k].GetGameTile());
                }
            }
        }

        return tilesInRangeToMoveAndAttack;
    }

    private List<GameTile> GetTilesInMovementRange(GameTile startingGridTile, int currentAP, bool ignoreTerrainDifferences, bool letPassEnemies)
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

            // if we have exceeded the allowed AP usage, end this branch of the loop
            if (current.F >= currentAP)
                continue;

            List<GameTile> adjacentSquares = current.GameTile.AdjacentTiles();

            foreach (var adjacentTile in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.X == adjacentTile.m_gridPosition.x
                        && l.Y == adjacentTile.m_gridPosition.y) != null)
                    continue;

                if (ignoreTerrainDifferences || !adjacentTile.IsPassable(startingGridTile.m_occupyingEntity, letPassEnemies))
                    continue;

                // if it's not in the open list...
                var adjacent = openList.FirstOrDefault(l => l.X == adjacentTile.m_gridPosition.x
                        && l.Y == adjacentTile.m_gridPosition.y);

                int g;
                if (ignoreTerrainDifferences)
                    g = current.G + 1;
                else
                    g = current.G + adjacentTile.GetCostToPass(startingGridTile.m_occupyingEntity);

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
            m_gridArray[i].SetAttackable(false);
        }
    }

    public void EndIntermissionFogUpdate()
    {
        for (int i = 0; i < m_gridArray.Length; i++)
        {
            if (!m_gridArray[i].GetGameTile().m_isFog)
            {
                m_gridArray[i].GetGameTile().m_isSoftFog = true;
                m_gridArray[i].GetGameTile().m_isFog = true;
            }
        }

        GamePlayer player = GameHelper.GetPlayer();

        for (int i = 0; i < player.m_controlledBuildings.Count; i++)
        {
            player.m_controlledBuildings[i].GetWorldTile().ClearSurroundingFog(player.m_controlledBuildings[i].m_sightRange);
        }
    }

    //============================================================================================================//

    public string SaveToJsonAsString()
    {
        JsonGridData jsonData = new JsonGridData
        {
            gridSizeX = m_gridSizeX,
            gridSizeY = m_gridSizeY,
            jsonTileData = new List<string>()
        };

        for (int i = 0; i < m_gridArray.Length; i++)
        {
            jsonData.jsonTileData.Add(m_gridArray[i].GetGameTile().SaveToJsonAsString());
        }

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public void LoadFromJson(JsonGridData jsonData)
    {
        m_jsonGridData = jsonData;
        if (m_setup)
        {
            RecycleGrid();
            SetupEmptyGrid(m_gridRoot, jsonData.gridSizeX, jsonData.gridSizeY);
            LoadJsonGrid(m_jsonGridData.Value); 
        }
    }
}
