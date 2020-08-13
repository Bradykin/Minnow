using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid
{
    private GridTile[] m_gridArray;

    public WorldGrid()
    {

    }

    public void SetupSquareGrid()
    {
        int numGridTiles = Constants.GridSizeX * Constants.GridSizeY;
        m_gridArray = new GridTile[numGridTiles];

        for (int i = 0; i < numGridTiles; i++)
        {
            m_gridArray[i] = new GridTile();
        }
    }

    public void SetupHexagonGrid()
    {
        int numGridTiles = 1;
        for (int i = 3; i < Constants.GridDiameter; i += 2)
        {
            numGridTiles += 6 * ((i - 1) / 2);
        }

        m_gridArray = new GridTile[numGridTiles];

        for (int i = 0; i < numGridTiles; i++)
        {
            m_gridArray[i] = new GridTile();
        }
    }
}
