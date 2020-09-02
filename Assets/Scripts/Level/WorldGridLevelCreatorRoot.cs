using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGridLevelCreatorRoot : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnGrid();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Globals.m_currentlyPaintingTerrain = GameTerrainFactory.GetNextTerrain(Globals.m_currentlyPaintingTerrain); 
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Globals.m_currentlyPaintingTerrain.GetNextSprite();
        }
    }

    public void SpawnGrid()
    {
        WorldGridManager.Instance.SetupEmptyGrid(transform);
    }

    public void SaveGrid()
    {
        WorldGridManager.Instance.RecycleGrid();
    }
}
