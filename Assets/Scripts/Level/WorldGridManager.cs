using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            GameHelper.MakePlayerBuilding(m_gridArray[Constants.GridSizeX * 3 + 5].m_gameTile, new ContentCastleBuilding());
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

        for (int i = 0; i < numGridTiles; i++)
        {
            m_gridArray[i] = FactoryManager.Instance.GetFactory<WorldTileFactory>().CreateObject<WorldTile>();
            m_gridArray[i].transform.parent = parent;

            int x = i % Constants.GridSizeX;
            int y = i / Constants.GridSizeX;

            m_gridArray[i].Init(x, y);
            m_gridArray[i].transform.position = m_gridArray[i].GetScreenPosition();
        }
    }

    //Range 1 = surrounding tiles (but not middle tiles)
    public List<WorldTile> GetSurroundingTiles(WorldTile middle, int range)
    {
        List<WorldTile> returnList = new List<WorldTile>();

        //TODO: ashulman: Can you fill in this function?  It's so that things like towers can hit surrounding tiles

        return returnList;
    }

    public WorldTile GetWorldGridTileAtPosition(int x, int y)
    {
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
                GameTile gameTile = WorldGridManager.Instance.m_gridArray[i].m_gameTile;
                gameTile.PlaceEntity(GameEnemyFactory.GetRandomEnemy());
                print(gameOpponent.m_controlledEntities);
                gameOpponent.m_controlledEntities.Add(gameTile.m_occupyingEntity);
            }
        }
    }

    //============================================================================================================//

    public int GetPathLength(GameTile startingGridTile, GameTile targetGridTile)
    {
        List<GameTile> path = CalculateAStarPath(startingGridTile, targetGridTile);

        int length = 0;
        for (int i = 1; i < path.Count; i++)
        {
            length += path[i].m_terrain.m_costToPass;
        }

        return length;
    }

    public List<GameTile> CalculateAStarPath(GameTile startingGridTile, GameTile targetGridTile)
    {
        Location current = null;
        int g = 0;
        var start = new Location(startingGridTile, targetGridTile, g, null);
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
                    path.Add(current.GridTile);
                    current = current.Parent;
                }
                path.Reverse();
                return path;
            }

            var adjacentSquares = current.GridTile.AdjacentTiles();
            g += current.GridTile.m_terrain.m_costToPass;

            foreach (var adjacentTile in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.X == adjacentTile.x
                        && l.Y == adjacentTile.y) != null)
                    continue;

                GameTile adjacentGridTile = GetWorldGridTileAtPosition(adjacentTile).m_gameTile;

                if (!adjacentGridTile.m_terrain.m_isPassable)
                    continue;

                // if it's not in the open list...
                var adjacent = openList.FirstOrDefault(l => l.X == adjacentTile.x
                        && l.Y == adjacentTile.y);
                if (adjacent == null)
                {
                    Location adjacentLocation = new Location(adjacentGridTile, targetGridTile, g, current);
                    openList.Insert(0, adjacentLocation);
                }
                else if (g + adjacent.H < adjacent.F)
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
}
