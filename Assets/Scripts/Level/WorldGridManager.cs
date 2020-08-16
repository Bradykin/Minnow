using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGridManager : MonoBehaviour, IReset
{
    private WorldGridTile[] m_gridArray;

    private bool m_setup;

    public GameObject m_testMovementObject;
    private WorldGridTile m_currentTile;

    void Start()
    {

    }

    void Update()
    {
        if (!m_setup)
            return;
        
        if (Input.GetKeyDown(KeyCode.A))
            m_currentTile = GetWorldGridTileAtPosition(m_currentTile.Left());

        if (Input.GetKeyDown(KeyCode.D))
            m_currentTile = GetWorldGridTileAtPosition(m_currentTile.Right());

        if (Input.GetKeyDown(KeyCode.W))
            m_currentTile = GetWorldGridTileAtPosition(m_currentTile.UpLeft());

        if (Input.GetKeyDown(KeyCode.E))
            m_currentTile = GetWorldGridTileAtPosition(m_currentTile.UpRight());

        if (Input.GetKeyDown(KeyCode.Z))
            m_currentTile = GetWorldGridTileAtPosition(m_currentTile.DownLeft());

        if (Input.GetKeyDown(KeyCode.X))
            m_currentTile = GetWorldGridTileAtPosition(m_currentTile.DownRight());

        m_testMovementObject.transform.position = m_currentTile.transform.position;
    }

    public void Activate()
    {
        if (!m_setup)
        {
            SetupSquareGrid();
            m_setup = true;

            m_currentTile = GetWorldGridTileAtPosition(3, 3);
            m_testMovementObject.transform.position = m_currentTile.transform.position;
        }
    }

    public void Reset()
    {

    }

    public void SetupSquareGrid()
    {
        int numGridTiles = Constants.GridSizeX * Constants.GridSizeY;
        m_gridArray = new WorldGridTile[numGridTiles];

        for (int i = 0; i < numGridTiles; i++)
        {
            m_gridArray[i] = FactoryManager.Instance.GetFactory<WorldGridTileFactory>().CreateObject<WorldGridTile>();

            int x = i % Constants.GridSizeX;
            int y = i / Constants.GridSizeX;

            m_gridArray[i].Init(x, y);
            m_gridArray[i].transform.position = new Vector3((x + y * 0.5f - y / 2) * Constants.HexagonInnerRadius * 2.0f, y * Constants.HexagonOuterRadius * 1.5f, 0.0f);
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
}
