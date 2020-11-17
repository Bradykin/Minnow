using Game.Util;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGridManager : Singleton<WorldGridManager>, ISave<JsonMapData>, ILoad<JsonMapData>
{
    public WorldTile[] m_gridArray { get; private set; }

    private JsonMapData? m_jsonMapData = null;

    private int m_gridSizeX;
    private int m_gridSizeY;

    private bool m_setup;

    private Transform m_gridRoot;

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

        if (m_jsonMapData.HasValue)
        {
            JsonMapData jsonMapData = m_jsonMapData.Value;
            SetupEmptyGrid(parent, jsonMapData.gridSizeX, jsonMapData.gridSizeY);
            LoadJsonGrid(jsonMapData);
        }
        else
        {
            SetupEmptyGrid(parent, Constants.GridSizeX, Constants.GridSizeY);
        }

        m_setup = true;
    }

    private void PlaceCrystals()
    {
        List<WorldTile> validTiles = new List<WorldTile>();
        List<WorldTile> tilesInRange = WorldGridManager.Instance.GetSurroundingWorldTiles(GameHelper.GetPlayer().GetCastleWorldTile(), Constants.GridSizeX, 12);

        for (int i = 0; i < tilesInRange.Count; i++)
        {
            if (tilesInRange[i].GetGameTile().IsPassable(null, false) && !tilesInRange[i].GetGameTile().GetTerrain().IsEventTerrain() && tilesInRange[i].GetGameTile().m_gameEventMarkers.Count == 0 && tilesInRange[i].GetGameTile().m_spawnPoint == null)
            {
                validTiles.Add(tilesInRange[i]);
            }
        }

        for (int i = 0; i < WorldController.Instance.m_gameController.m_map.GetNumCrystals(); i++)
        {
            int r = UnityEngine.Random.Range(0, validTiles.Count);
            validTiles[r].GetGameTile().PlaceBuilding(new ContentPowerCrystalBuilding());
            validTiles.RemoveAt(r);
        }
    }

    private void PlaceChests()
    {
        //Place common chests
        List<WorldTile> validTiles = new List<WorldTile>();

        List<WorldTile> tilesInRangeForCommon = WorldGridManager.Instance.GetSurroundingWorldTiles(GameHelper.GetPlayer().GetCastleWorldTile(), 9, 4);

        for (int i = 0; i < tilesInRangeForCommon.Count; i++)
        {
            if (TileValidForWorldPerk(tilesInRangeForCommon[i].GetGameTile()))
            {
                validTiles.Add(tilesInRangeForCommon[i]);
            }
        }

        for (int i = 0; i < Constants.NumCommonChests; i++)
        {
            int r = UnityEngine.Random.Range(0, validTiles.Count);
            validTiles[r].GetGameTile().m_gameWorldPerk = new GameWorldPerk(validTiles[r].GetGameTile(), GameElementBase.GameRarity.Common);

            validTiles.RemoveAt(r);
        }

        //Place uncommon chests
        validTiles = new List<WorldTile>();

        List<WorldTile> tilesInRangeForUncommon = WorldGridManager.Instance.GetSurroundingWorldTiles(GameHelper.GetPlayer().GetCastleWorldTile(), 15, 8);

        for (int i = 0; i < tilesInRangeForUncommon.Count; i++)
        {
            if (TileValidForWorldPerk(tilesInRangeForUncommon[i].GetGameTile()))
            {
                validTiles.Add(tilesInRangeForUncommon[i]);
            }
        }

        for (int i = 0; i < Constants.NumUncommonChests; i++)
        {
            int r = UnityEngine.Random.Range(0, validTiles.Count);
            validTiles[r].GetGameTile().m_gameWorldPerk = new GameWorldPerk(validTiles[r].GetGameTile(), GameElementBase.GameRarity.Uncommon);

            validTiles.RemoveAt(r);
        }

        //Place rare chests
        validTiles = new List<WorldTile>();

        List<WorldTile> tilesInRangeForRare = WorldGridManager.Instance.GetSurroundingWorldTiles(GameHelper.GetPlayer().GetCastleWorldTile(), Constants.GridSizeX, 15);

        for (int i = 0; i < tilesInRangeForRare.Count; i++)
        {
            if (TileValidForWorldPerk(tilesInRangeForRare[i].GetGameTile()))
            {
                validTiles.Add(tilesInRangeForRare[i]);
            }
        }

        for (int i = 0; i < Constants.NumRareChests; i++)
        {
            int r = UnityEngine.Random.Range(0, validTiles.Count);
            validTiles[r].GetGameTile().m_gameWorldPerk = new GameWorldPerk(validTiles[r].GetGameTile(), GameElementBase.GameRarity.Rare);

            validTiles.RemoveAt(r);
        }
    }

    private void PlaceGold()
    {
        //Place close gold
        List<WorldTile> validTiles = new List<WorldTile>();

        List<WorldTile> tilesInRangeForCloseGold = WorldGridManager.Instance.GetSurroundingWorldTiles(GameHelper.GetPlayer().GetCastleWorldTile(), 7, 3);

        for (int i = 0; i < tilesInRangeForCloseGold.Count; i++)
        {
            if (TileValidForWorldPerk(tilesInRangeForCloseGold[i].GetGameTile()))
            {
                validTiles.Add(tilesInRangeForCloseGold[i]);
            }
        }

        for (int i = 0; i < Constants.NumCloseGold; i++)
        {
            int r = UnityEngine.Random.Range(0, validTiles.Count);
            validTiles[r].GetGameTile().m_gameWorldPerk = new GameWorldPerk(validTiles[r].GetGameTile(), Constants.CloseGoldVal);

            validTiles.RemoveAt(r);
        }

        //Place farther gold
        validTiles = new List<WorldTile>();

        List<WorldTile> tilesInRangeForFarGold = WorldGridManager.Instance.GetSurroundingWorldTiles(GameHelper.GetPlayer().GetCastleWorldTile(), 16, 8);

        for (int i = 0; i < tilesInRangeForFarGold.Count; i++)
        {
            if (TileValidForWorldPerk(tilesInRangeForFarGold[i].GetGameTile()))
            {
                validTiles.Add(tilesInRangeForFarGold[i]);
            }
        }

        for (int i = 0; i < Constants.NumFarGold; i++)
        {
            int r = UnityEngine.Random.Range(0, validTiles.Count);
            validTiles[r].GetGameTile().m_gameWorldPerk = new GameWorldPerk(validTiles[r].GetGameTile(), Constants.FarGoldVal);

            validTiles.RemoveAt(r);
        }
    }

    public void PlaceAltars()
    {
        //Place 
        List<WorldTile> validTiles = new List<WorldTile>();

        List<WorldTile> tilesInRange = WorldGridManager.Instance.GetSurroundingWorldTiles(GameHelper.GetPlayer().GetCastleWorldTile(), Constants.GridSizeX, 15);

        for (int i = 0; i < tilesInRange.Count; i++)
        {
            if (TileValidForWorldPerk(tilesInRange[i].GetGameTile()))
            {
                validTiles.Add(tilesInRange[i]);
            }
        }

        int r = UnityEngine.Random.Range(0, validTiles.Count);
        validTiles[r].GetGameTile().m_gameWorldPerk = new GameWorldPerk(validTiles[r].GetGameTile(), new ContentTelloAltar(validTiles[r].GetGameTile()));
        validTiles.RemoveAt(r);

        r = UnityEngine.Random.Range(0, validTiles.Count);
        validTiles[r].GetGameTile().m_gameWorldPerk = new GameWorldPerk(validTiles[r].GetGameTile(), new ContentDorphinAltar(validTiles[r].GetGameTile()));
        validTiles.RemoveAt(r);

        r = UnityEngine.Random.Range(0, validTiles.Count);
        validTiles[r].GetGameTile().m_gameWorldPerk = new GameWorldPerk(validTiles[r].GetGameTile(), new ContentMonAltar(validTiles[r].GetGameTile()));
        validTiles.RemoveAt(r);

        r = UnityEngine.Random.Range(0, validTiles.Count);
        validTiles[r].GetGameTile().m_gameWorldPerk = new GameWorldPerk(validTiles[r].GetGameTile(), new ContentSugoAltar(validTiles[r].GetGameTile()));
        validTiles.RemoveAt(r);
    }

    public void ClearAltars()
    {
        for (int i= 0; i < m_gridArray.Length; i++)
        {
            if (m_gridArray[i].GetGameTile().m_gameWorldPerk != null && m_gridArray[i].GetGameTile().m_gameWorldPerk.IsAltar())
            {
                m_gridArray[i].GetGameTile().m_gameWorldPerk = null;
            }
        }
    }

    private bool TileValidForWorldPerk(GameTile toCheck)
    {
        if (!toCheck.IsPassable(null, false))
        {
            return false;
        }

        if (toCheck.m_gameWorldPerk != null)
        {
            return false;
        }

        if (toCheck.HasBuilding())
        {
            return false;
        }

        //Removing this means that tiles can be both worldperks and affected by map events, such as flooding. If this proves not to be fun, re-evaluate
        /*if (toCheck.m_gameEventMarkers.Count != 0)
        {
            return false;
        }*/

        if (toCheck.m_spawnPoint != null)
        {
            return false;
        }

        return true;
    }

    public void SetupEmptyGrid(Transform parent, int gridSizeX, int gridSizeY)
    {
        if (m_setup)
        {
            RecycleGrid();
        }

        SetupGrid(parent, gridSizeX, gridSizeY);
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

    private void SetupGrid(Transform parent, int gridSizeX, int gridSizeY)
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

            int x = i % Constants.GridSizeX;
            int y = i / Constants.GridSizeX;

            m_gridArray[i].Init(x, y);
            m_gridArray[i].transform.position = m_gridArray[i].GetScreenPosition();
            m_gridArray[i].m_renderer.sortingOrder = y;
            m_gridArray[i].m_tintRenderer.sortingOrder = y;
            m_gridArray[i].m_frameRenderer.sortingOrder = y;
        }
    }

    private void LoadJsonGrid(JsonMapData jsonMapData)
    {
        for (int i = 0; i < jsonMapData.jsonGameTileData.Count; i++)
        {
            JsonGameTileData jsonGameTileData = jsonMapData.jsonGameTileData[i];
            GetWorldGridTileAtPosition(new Vector2Int(jsonGameTileData.gridPositionX, jsonGameTileData.gridPositionY)).GetGameTile().LoadFromJson(jsonGameTileData);
        }
    }

    public List<GameTile> GetSurroundingGameTiles(GameTile centerPoint, int outerRange, int innerRange = 1)
    {
        List<WorldTile> worldTiles = GetSurroundingWorldTiles(centerPoint.GetWorldTile(), outerRange, innerRange);

        List<GameTile> returnList = new List<GameTile>();
        
        for (int i = 0; i < worldTiles.Count; i++)
        {
            returnList.Add(worldTiles[i].GetGameTile());
        }

        return returnList;
    }

    //Range 1 = surrounding tiles (but not middle tiles)
    public List<WorldTile> GetSurroundingWorldTiles(WorldTile centerPoint, int outerRange, int innerRange = 1)
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

    public List<GameTile> GetFogBorderGameTiles()
    {
        List<WorldTile> worldTiles = GetFogBorderWorldTiles();

        List<GameTile> returnList = new List<GameTile>();

        for (int i = 0; i < worldTiles.Count; i++)
        {
            returnList.Add(worldTiles[i].GetGameTile());
        }

        return returnList;
    }

    public List<WorldTile> GetFogBorderWorldTiles()
    {
        List<WorldTile> returnList = new List<WorldTile>();

        for (int i = 0; i < m_gridArray.Length; i++)
        {
            if (m_gridArray[i].GetGameTile().m_isFogBorder)
            {
                returnList.Add(m_gridArray[i]);
            }
        }

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
            {
                returnList.Add(currentWorldTile);
            }
        }
        for (int i = 0; i < range; i++)
        {
            currentTileCoordinates = currentTileCoordinates.RightCoordinate();
            currentWorldTile = GetWorldGridTileAtPosition(currentTileCoordinates);
            if (currentWorldTile != null)
            {
                returnList.Add(currentWorldTile);
            }
        }
        for (int i = 0; i < range; i++)
        {
            currentTileCoordinates = currentTileCoordinates.DownRightCoordinate();
            currentWorldTile = GetWorldGridTileAtPosition(currentTileCoordinates);
            if (currentWorldTile != null)
            {
                returnList.Add(currentWorldTile);
            }
        }
        for (int i = 0; i < range; i++)
        {
            currentTileCoordinates = currentTileCoordinates.DownLeftCoordinate();
            currentWorldTile = GetWorldGridTileAtPosition(currentTileCoordinates);
            if (currentWorldTile != null)
            {
                returnList.Add(currentWorldTile);
            }
        }
        for (int i = 0; i < range; i++)
        {
            currentTileCoordinates = currentTileCoordinates.LeftCoordinate();
            currentWorldTile = GetWorldGridTileAtPosition(currentTileCoordinates);
            if (currentWorldTile != null)
            {
                returnList.Add(currentWorldTile);
            }
        }
        for (int i = 0; i < range; i++)
        {
            currentTileCoordinates = currentTileCoordinates.UpLeftCoordinate();
            currentWorldTile = GetWorldGridTileAtPosition(currentTileCoordinates);
            if (currentWorldTile != null)
            {
                returnList.Add(currentWorldTile);
            }
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
            if (curTile.m_occupyingUnit != null)
            {
                if (curTile.m_occupyingUnit.GetTeam() == Team.Player)
                {
                    gamePlayer.AddControlledUnit(curTile.m_occupyingUnit);
                }
                else if (curTile.m_occupyingUnit.GetTeam() == Team.Enemy && curTile.m_occupyingUnit is GameEnemyUnit gameEnemyUnit)
                {
                    gameOpponent.AddControlledUnit(gameEnemyUnit);
                }
                else
                {
                    Debug.LogError("Problem loading units from world grid - did not match previous criteria");
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

        if (!Globals.loadingRun)
        {
            if (WorldController.Instance.m_gameController.GetCurMap().GetShouldSpawnCrystals())
            {
                PlaceCrystals();
            }
            PlaceChests();
            PlaceGold();
        }

        UICameraController.Instance.SnapToGameObject(WorldController.Instance.m_gameController.m_player.GetCastleWorldTile().gameObject);
    }

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
                length += path[i].GetCostToPass(startingGridTile.m_occupyingUnit);
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
                if (currentPosition.x > targetPosition.x)
                    currentPosition = currentPosition.DownLeftCoordinate();
                else if (currentPosition.x < targetPosition.x)
                    currentPosition = currentPosition.DownRightCoordinate();
                else if (currentPosition.y % 2 != 0)
                    currentPosition = currentPosition.DownLeftCoordinate();
                else
                    currentPosition = currentPosition.DownRightCoordinate();
            }
            else
            {
                if (currentPosition.x > targetPosition.x)
                    currentPosition = currentPosition.UpLeftCoordinate();
                else if (currentPosition.x < targetPosition.x)
                    currentPosition = currentPosition.UpRightCoordinate();
                else if (currentPosition.y % 2 != 0)
                    currentPosition = currentPosition.UpLeftCoordinate();
                else
                    currentPosition = currentPosition.UpRightCoordinate();
            }
            distance++;
        }

        return distance + Math.Abs(currentPosition.x - targetPosition.x);
    }


    //What is this function?
    public List<GameTile> CalculatePathTowards(GameTile startingGridTile, GameTile targetGridTile, bool ignoreTerrainDifferences, bool getAdjacentPosition, int curStamina)
    {
        if (startingGridTile.IsOccupied() && !startingGridTile.m_occupyingUnit.m_isDead && startingGridTile.m_occupyingUnit.GetRootedKeyword() != null)
        {
            return null;
        }

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

                if (!ignoreTerrainDifferences && !adjacentTile.IsPassable(startingGridTile.m_occupyingUnit, current.F >= curStamina))
                    continue;

                // if it's not in the open list...
                var adjacent = openList.Find(l => l.X == adjacentTile.m_gridPosition.x
                        && l.Y == adjacentTile.m_gridPosition.y);

                int g;
                if (ignoreTerrainDifferences)
                    g = current.G + 1;
                else
                    g = current.G + adjacentTile.GetCostToPass(startingGridTile.m_occupyingUnit);

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
        if (startingGridTile.IsOccupied() && !startingGridTile.m_occupyingUnit.m_isDead && startingGridTile.m_occupyingUnit.GetRootedKeyword() != null)
        {
            return null;
        }

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

                if (!ignoreTerrainDifferences && !adjacentTile.IsPassable(startingGridTile.m_occupyingUnit, letPassEnemies))
                    continue;

                // if it's not in the open list...
                var adjacent = openList.Find(l => l.X == adjacentTile.m_gridPosition.x
                        && l.Y == adjacentTile.m_gridPosition.y);

                int g;
                if (ignoreTerrainDifferences)
                    g = current.G + 1;
                else
                    g = current.G + adjacentTile.GetCostToPass(startingGridTile.m_occupyingUnit);

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
        if (startingGridTile == null || !startingGridTile.IsOccupied() || startingGridTile.m_occupyingUnit.m_isDead)
        {
            Debug.Log("NO UNIT ON TILE");
            return new List<GameTile>();
        }
        return GetTilesInMovementRange(startingGridTile, startingGridTile.m_occupyingUnit.GetCurStamina(), ignoreTerrainDifferences, letPassEnemies);
    }

    public List<GameTile> GetTilesInMovementRangeWithStaminaToAttack(GameTile startingGridTile, bool ignoreTerrainDifferences, bool letPassEnemies)
    {
        if (startingGridTile == null || !startingGridTile.IsOccupied() || startingGridTile.m_occupyingUnit.m_isDead)
        {
            Debug.Log("NO UNIT ON TILE");
            return null;
        }
        int movementStamina = startingGridTile.m_occupyingUnit.GetCurStamina() - startingGridTile.m_occupyingUnit.GetStaminaToAttack();

        List<GameTile> tilesInMovementRangeWithStaminaToAttack = GetTilesInMovementRange(startingGridTile, movementStamina, ignoreTerrainDifferences, letPassEnemies);
        return tilesInMovementRangeWithStaminaToAttack;
    }

    public List<GameTile> GetTilesInRangeToMoveAndAttack(GameTile startingGridTile, bool ignoreTerrainDifferences, bool letPassEnemies)
    {
        if (startingGridTile == null || !startingGridTile.IsOccupied() || startingGridTile.m_occupyingUnit.m_isDead)
        {
            Debug.Log("NO UNIT ON TILE");
            return null;
        }

        List<GameTile> tilesInMovementRangeWithStaminaToAttack = GetTilesInMovementRangeWithStaminaToAttack(startingGridTile, ignoreTerrainDifferences, letPassEnemies);
        if (tilesInMovementRangeWithStaminaToAttack == null)
        {
            tilesInMovementRangeWithStaminaToAttack = new List<GameTile>();
            tilesInMovementRangeWithStaminaToAttack.Add(startingGridTile);
        }

        for (int i = tilesInMovementRangeWithStaminaToAttack.Count - 1; i >= 0; i--)
        {
            if (tilesInMovementRangeWithStaminaToAttack[i].IsOccupied() && !tilesInMovementRangeWithStaminaToAttack[i].m_occupyingUnit.m_isDead && tilesInMovementRangeWithStaminaToAttack[i] != startingGridTile)
            {
                tilesInMovementRangeWithStaminaToAttack.RemoveAt(i);
            }
        }

        int range = startingGridTile.m_occupyingUnit.GetRange();

        List<GameTile> tilesInRangeToMoveAndAttack = new List<GameTile>();

        for (int i = 0; i < tilesInMovementRangeWithStaminaToAttack.Count; i++)
        {
            if (tilesInMovementRangeWithStaminaToAttack[i].IsOccupied() && !tilesInMovementRangeWithStaminaToAttack[i].m_occupyingUnit.m_isDead && tilesInMovementRangeWithStaminaToAttack[i] != startingGridTile)
            {
                continue;
            }
            
            List<WorldTile> tiles = GetSurroundingWorldTiles(GetWorldGridTileAtPosition(tilesInMovementRangeWithStaminaToAttack[i].m_gridPosition), range);
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

    private List<GameTile> GetTilesInMovementRange(GameTile startingGridTile, int currentStamina, bool ignoreTerrainDifferences, bool letPassEnemies)
    {
        if (startingGridTile.IsOccupied() && !startingGridTile.m_occupyingUnit.m_isDead && startingGridTile.m_occupyingUnit.GetRootedKeyword() != null)
        {
            return null;
        }

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

            // if we have exceeded the allowed Stamina usage, end this branch of the loop
            if (current.F >= currentStamina)
                continue;

            List<GameTile> adjacentSquares = current.GameTile.AdjacentTiles();

            foreach (var adjacentTile in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.X == adjacentTile.m_gridPosition.x
                        && l.Y == adjacentTile.m_gridPosition.y) != null)
                    continue;

                if (ignoreTerrainDifferences || !adjacentTile.IsPassable(startingGridTile.m_occupyingUnit, letPassEnemies))
                    continue;

                // if it's not in the open list...
                var adjacent = openList.FirstOrDefault(l => l.X == adjacentTile.m_gridPosition.x
                        && l.Y == adjacentTile.m_gridPosition.y);

                int g;
                if (ignoreTerrainDifferences)
                    g = current.G + 1;
                else
                    g = current.G + adjacentTile.GetCostToPass(startingGridTile.m_occupyingUnit);

                if (g > currentStamina)
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

        List<Location> inRangeLocations = closedList.FindAll(l => l.G <= currentStamina);
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

    public void ClearAllTilesSpellcraftRange()
    {
        for (int i = 0; i < m_gridArray.Length; i++)
        {
            m_gridArray[i].ClearSpellcraftRangeCount();
        }
    }

    public void ClearAllTilesDefensiveBuildingRange()
    {
        for (int i = 0; i < m_gridArray.Length; i++)
        {
            m_gridArray[i].ClearInDefensiveBuildingRangeCount();
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
            m_gridArray[i].GetGameTile().m_isFogBorder = false;
        }

        GameController gameController = GameHelper.GetGameController();
        GamePlayer player = GameHelper.GetPlayer();

        for (int i = 0; i < player.m_controlledBuildings.Count; i++)
        {
            player.m_controlledBuildings[i].GetWorldTile().ClearSurroundingFog(player.m_controlledBuildings[i].GetSightRange());
        }

        for (int i = 0; i < player.m_controlledUnits.Count; i++)
        {
            player.m_controlledUnits[i].GetWorldTile().ClearSurroundingFog(player.m_controlledUnits[i].GetSightRange());
        }

        ContentLordOfShadowsEnemy lordOfShadowsEnemy = GameHelper.GetBoss<ContentLordOfShadowsEnemy>();
        if (lordOfShadowsEnemy != null && lordOfShadowsEnemy.m_brightnessLevel > 0)
        {
            lordOfShadowsEnemy.GetWorldTile().ClearSurroundingFog(lordOfShadowsEnemy.m_brightnessLevel);
        }
    }

    //============================================================================================================//

    public JsonMapData SaveToJson()
    {
        JsonMapData jsonData = new JsonMapData
        {
            gridSizeX = m_gridSizeX,
            gridSizeY = m_gridSizeY,
            jsonGameTileData = new List<JsonGameTileData>()
        };

        for (int i = 0; i < m_gridArray.Length; i++)
        {
            jsonData.jsonGameTileData.Add(m_gridArray[i].GetGameTile().SaveToJson());
        }

        return jsonData;
    }

    public void LoadFromJson(JsonMapData jsonData)
    {
        m_jsonMapData = jsonData;
        if (m_setup)
        {
            RecycleGrid();
            SetupEmptyGrid(m_gridRoot, jsonData.gridSizeX, jsonData.gridSizeY);
            LoadJsonGrid(m_jsonMapData.Value);
        }
    }
}
