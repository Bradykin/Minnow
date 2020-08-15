using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGridManager : MonoBehaviour
{
    private WorldGridTile[] m_gridArray;

    [SerializeField]
    private WorldGridTile m_gridTilePrefab;

    void Start()
    {
        SetupSquareGrid();
    }

    public void SetupSquareGrid()
    {
        int numGridTiles = Constants.GridSizeX * Constants.GridSizeY;
        m_gridArray = new WorldGridTile[numGridTiles];

        for (int i = 0; i < numGridTiles; i++)
        {
            m_gridArray[i] = GameObject.Instantiate(m_gridTilePrefab);

            int x = i % Constants.GridSizeX;
            int y = i / Constants.GridSizeX;

            m_gridArray[i].Init(x, y);
            m_gridArray[i].transform.position = new Vector3((x + y * 0.5f - y / 2) * Constants.HexagonInnerRadius * 2.0f, y * Constants.HexagonOuterRadius * 1.5f, 0.0f);
        }
    }
}
