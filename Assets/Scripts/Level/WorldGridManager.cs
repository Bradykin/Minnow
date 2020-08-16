using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldGridManager : Singleton<WorldGridManager>
{
    private WorldGridTile[] m_gridArray;

    private bool m_setup;

    void Start()
    {
        
    }

    void Update()
    {
        if (!m_setup)
            return;

        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneLoader.ActivateScene("AlexTestScene", SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneLoader.ActivateScene("NickTestScene", SceneManager.GetActiveScene().name);
        }

        /*if (Input.GetKeyDown(KeyCode.Y))
        {
            List<WorldGridTile> AStarPath = CalculateAStarPath(GetWorldGridTileAtPosition(1, 1), GetWorldGridTileAtPosition(3, 7));
            foreach (var tile in AStarPath)
            {
                GameObject.Instantiate(m_testMovementObjectPrefab).transform.position = tile.transform.position;
            }
        }*/
    }

    public void Setup(Transform parent)
    {
        if (!m_setup)
        {
            SetupSquareGrid(parent);
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
                Recycler.Recycle<WorldGridTile>(tile);
            }
        }
    }

    private void SetupSquareGrid(Transform parent)
    {
        int numGridTiles = Constants.GridSizeX * Constants.GridSizeY;
        m_gridArray = new WorldGridTile[numGridTiles];

        for (int i = 0; i < numGridTiles; i++)
        {
            m_gridArray[i] = FactoryManager.Instance.GetFactory<WorldGridTileFactory>().CreateObject<WorldGridTile>();
            m_gridArray[i].transform.parent = parent;

            int x = i % Constants.GridSizeX;
            int y = i / Constants.GridSizeX;

            m_gridArray[i].Init(x, y);
            m_gridArray[i].transform.position = UIHelper.GetScreenPositionForWorldGridElement(x, y);
        }
    }

    public WorldGridTile GetWorldGridTileAtPosition(int x, int y)
    {
        return m_gridArray[x + y * Constants.GridSizeX];
    }

    public WorldGridTile GetWorldGridTileAtPosition(Vector2Int position)
    {
        return GetWorldGridTileAtPosition(position.x, position.y);
    }

    //============================================================================================================//

    public List<WorldGridTile> CalculateAStarPath(WorldGridTile startingGridTile, WorldGridTile targetGridTile)
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
                List<WorldGridTile> path = new List<WorldGridTile>();
                while (current != null)
                {
                    path.Add(current.GridTile);
                    current = current.Parent;
                }
                path.Reverse();
                return path;
            }

            var adjacentSquares = current.GridTile.AdjacentTiles();
            g += current.GridTile.CostToPass;

            foreach (var adjacentTile in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.X == adjacentTile.x
                        && l.Y == adjacentTile.y) != null)
                    continue;

                WorldGridTile adjacentGridTile = GetWorldGridTileAtPosition(adjacentTile);

                if (!adjacentGridTile.IsPassable)
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
