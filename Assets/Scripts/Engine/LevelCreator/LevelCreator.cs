using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject m_worldGridLevelCreatorRoot;
    [SerializeField]
    private Image m_selectedImage;

    private void Start()
    {
        Globals.m_currentlyPaintingTerrain = GameTerrainFactory.GetTerrainClone(new ContentForestTerrain());
        m_selectedImage.sprite = Globals.m_currentlyPaintingTerrain.m_icon;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnGrid();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Globals.m_currentlyPaintingTerrain = GameTerrainFactory.GetNextTerrain(Globals.m_currentlyPaintingTerrain);
            m_selectedImage.sprite = Globals.m_currentlyPaintingTerrain.m_icon;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            SaveGrid();
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
