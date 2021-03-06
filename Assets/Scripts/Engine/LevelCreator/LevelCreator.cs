﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Game.Util;
using Newtonsoft.Json;

public class LevelCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject m_worldGridLevelCreatorRoot = null;
    [SerializeField]
    private Image m_selectedImage = null;
    [SerializeField]
    private Text m_saveFileNotifier = null;
    [SerializeField]
    private Text m_selectedListNotifier = null;
    [SerializeField]
    private Text m_selectedTileNotifier = null;

    private string dataPath;

    private List<string> dataPaths;

    public int m_curPathIndex = -1;

    private void Start()
    {
        dataPaths = new List<string>();
        for (int i = 0; i < 20; i++)
        {
            dataPaths.Add(Files.MAP_DATA_PATH + i + ".txt");
        }
        Globals.m_currentlyPaintingType = typeof(GameTerrainBase);
        Globals.m_currentlyPaintingBuilding = new ContentCastleBuilding();
        Globals.m_currentlyPaintingTerrain = GameTerrainFactory.GetCurrentTerrain();

        m_selectedImage.sprite = Globals.m_currentlyPaintingTerrain.m_icon;
        m_selectedListNotifier.text = GameTerrainFactory.GetCurrentTerrainListName();
        m_selectedTileNotifier.text = GameTerrainFactory.GetCurrentTerrainName();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Globals.m_currentlyPaintingType == typeof(GameTerrainBase))
            {
                Globals.m_currentlyPaintingTerrain = GameTerrainFactory.GetNextTerrainList();
                m_selectedImage.sprite = Globals.m_currentlyPaintingTerrain.m_icon;
                m_selectedListNotifier.text = GameTerrainFactory.GetCurrentTerrainListName();
                m_selectedTileNotifier.text = GameTerrainFactory.GetCurrentTerrainName();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Globals.m_currentlyPaintingType == typeof(GameTerrainBase))
            {
                Globals.m_currentlyPaintingTerrain = GameTerrainFactory.GetNextTerrain();
                m_selectedImage.sprite = Globals.m_currentlyPaintingTerrain.m_icon;
                m_selectedTileNotifier.text = Globals.m_currentlyPaintingTerrain.GetName();
            }
            else if (Globals.m_currentlyPaintingType == typeof(GameBuildingBase))
            {
                Globals.m_currentlyPaintingBuilding = GameBuildingFactory.GetNextBuilding(Globals.m_currentlyPaintingBuilding);
                m_selectedImage.sprite = Globals.m_currentlyPaintingBuilding.m_icon;
                m_selectedTileNotifier.text = Globals.m_currentlyPaintingBuilding.GetName();
            }
            else if (Globals.m_currentlyPaintingType == typeof(GameSpawnPoint))
            {
                Globals.m_currentlyPaintingNumberIndex++;
                if (Globals.m_currentlyPaintingNumberIndex > 10)
                {
                    Globals.m_currentlyPaintingNumberIndex = 0;
                }

                if (Globals.m_currentlyPaintingNumberIndex == 0)
                {
                    m_selectedTileNotifier.text = "Default";
                }
                else
                {
                    m_selectedTileNotifier.text = "" + Globals.m_currentlyPaintingNumberIndex;
                }
            }
            else if (Globals.m_currentlyPaintingType == typeof(int))
            {
                Globals.m_currentlyPaintingNumberIndex++;
                if (Globals.m_currentlyPaintingNumberIndex > 10)
                {
                    Globals.m_currentlyPaintingNumberIndex = 0;
                }

                if (Globals.m_currentlyPaintingNumberIndex == 0)
                {
                    m_selectedTileNotifier.text = "Default";
                }
                else
                {
                    m_selectedTileNotifier.text = "" + Globals.m_currentlyPaintingNumberIndex;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Globals.m_currentlyPaintingType == typeof(GameTerrainBase))
            {
                Globals.m_currentlyPaintingTerrain = GameTerrainFactory.GetPreviousTerrain();
                m_selectedImage.sprite = Globals.m_currentlyPaintingTerrain.m_icon;
                m_selectedTileNotifier.text = Globals.m_currentlyPaintingTerrain.GetName();
            }
            else if (Globals.m_currentlyPaintingType == typeof(GameBuildingBase))
            {
                Globals.m_currentlyPaintingBuilding = GameBuildingFactory.GetPreviousBuilding(Globals.m_currentlyPaintingBuilding);
                m_selectedImage.sprite = Globals.m_currentlyPaintingBuilding.m_icon;
                m_selectedTileNotifier.text = Globals.m_currentlyPaintingBuilding.GetName();
            }
            else if (Globals.m_currentlyPaintingType == typeof(GameSpawnPoint))
            {
                Globals.m_currentlyPaintingNumberIndex--;
                if (Globals.m_currentlyPaintingNumberIndex < 0)
                {
                    Globals.m_currentlyPaintingNumberIndex = 10;
                }

                if (Globals.m_currentlyPaintingNumberIndex == 0)
                {
                    m_selectedTileNotifier.text = "Default";
                }
                else
                {
                    m_selectedTileNotifier.text = "" + Globals.m_currentlyPaintingNumberIndex;
                }
            }
            else if (Globals.m_currentlyPaintingType == typeof(int))
            {
                Globals.m_currentlyPaintingNumberIndex--;
                if (Globals.m_currentlyPaintingNumberIndex < 0)
                {
                    Globals.m_currentlyPaintingNumberIndex = 10;
                }

                if (Globals.m_currentlyPaintingNumberIndex == 0)
                {
                    m_selectedTileNotifier.text = "Default";
                }
                else
                {
                    m_selectedTileNotifier.text = "" + Globals.m_currentlyPaintingNumberIndex;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Globals.m_currentlyPaintingType == typeof(GameTerrainBase))
            {
                Globals.m_currentlyPaintingType = typeof(GameBuildingBase);
                m_selectedImage.sprite = Globals.m_currentlyPaintingBuilding.m_icon;
                m_selectedListNotifier.text = "Buildings";
                m_selectedTileNotifier.text = Globals.m_currentlyPaintingBuilding.GetName();
            }
            else if (Globals.m_currentlyPaintingType == typeof(GameBuildingBase))
            {
                Globals.m_currentlyPaintingType = typeof(GameSpawnPoint);
                m_selectedImage.sprite = null;
                m_selectedListNotifier.text = "Spawn point";
                Globals.m_currentlyPaintingNumberIndex = 0;


                if (Globals.m_currentlyPaintingNumberIndex == 0)
                {
                    m_selectedTileNotifier.text = "Default";
                }
                else
                {
                    m_selectedTileNotifier.text = "" + Globals.m_currentlyPaintingNumberIndex;
                }
            }
            else if (Globals.m_currentlyPaintingType == typeof(GameSpawnPoint))
            {
                Globals.m_currentlyPaintingType = typeof(int);
                m_selectedImage.sprite = null;
                m_selectedListNotifier.text = "Event Marker";
                Globals.m_currentlyPaintingNumberIndex = 0;


                if (Globals.m_currentlyPaintingNumberIndex == 0)
                {
                    m_selectedTileNotifier.text = "Default";
                }
                else
                {
                    m_selectedTileNotifier.text = "" + Globals.m_currentlyPaintingNumberIndex;
                }
            }
            else if (Globals.m_currentlyPaintingType == typeof(int))
            {
                Globals.m_currentlyPaintingType = typeof(GameWorldPerk);
                m_selectedImage.sprite = null;
                m_selectedListNotifier.text = "Event Marker";
                Globals.m_currentlyPaintingNumberIndex = 0;

                m_selectedTileNotifier.text = "Event";
            }
            else if (Globals.m_currentlyPaintingType == typeof(GameWorldPerk))
            {
                Globals.m_currentlyPaintingType = typeof(GameTerrainBase);
                Globals.m_currentlyPaintingTerrain = GameTerrainFactory.GetCurrentTerrain();
                m_selectedImage.sprite = Globals.m_currentlyPaintingTerrain.m_icon;
                m_selectedListNotifier.text = GameTerrainFactory.GetCurrentTerrainListName();
                m_selectedTileNotifier.text = GameTerrainFactory.GetCurrentTerrainName();
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Globals.m_levelCreatorEraserMode = !Globals.m_levelCreatorEraserMode;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneLoader.ActivateScene("LevelSelectScene", "LevelCreatorScene");
        }
    }

    public void SpawnGrid()
    {
        WorldGridManager.Instance.SetupEmptyGrid(transform, Constants.GridSizeX, Constants.GridSizeY);
    }

    public void LoadGrid(int pathIndex)
    {
        m_curPathIndex = pathIndex;
        m_saveFileNotifier.text = $"Save File: {pathIndex}";

#if UNITY_EDITOR
        string path = Path.Combine(Files.EDITOR_PATH, dataPaths[pathIndex]);
#else
        string path = Path.Combine(Files.BUILD_PATH, dataPaths[pathIndex]);
#endif

        if (!File.Exists(path))
        {
            SpawnGrid();
            return;
        }

        JsonMapData jsonData = JsonConvert.DeserializeObject<JsonMapData>(File.ReadAllText(path));

        WorldGridManager.Instance.LoadFromJson(jsonData);
        WorldGridManager.Instance.Setup(m_worldGridLevelCreatorRoot.transform);
    }

    public void SaveGrid(int pathIndex)
    {
        m_curPathIndex = -1;
        JsonMapData jsonMapData = WorldGridManager.Instance.SaveToJson();
        var export = JsonConvert.SerializeObject(jsonMapData);
#if UNITY_EDITOR
        File.WriteAllText(Path.Combine(Files.EDITOR_PATH, dataPaths[pathIndex]), export);
#else
        File.WriteAllText(Path.Combine(Files.BUILD_PATH, dataPaths[pathIndex]), export);
#endif

        List<JsonMapMetaData> jsonMapMetaData = Globals.LoadMapMetaData();
        bool containsMapData = jsonMapMetaData.Any(m => m.dataPath == dataPaths[pathIndex]);

        JsonMapMetaData newJsonData = new JsonMapMetaData
        {
            mapName = dataPaths[pathIndex],
            mapID = pathIndex,
            gridSize = new Vector2Int(Constants.GridSizeX, Constants.GridSizeY),
            mapDifficulty = (int)MapDifficulty.Easy,
            dataPath = dataPaths[pathIndex]
        };

        if (containsMapData)
        {
            //int indexOf = jsonMapMetaData.IndexOf(jsonMapMetaData.First(m => m.dataPath == dataPaths[pathIndex]));

            //jsonMapMetaData[indexOf] = newJsonData;
        }
        else
        {
            jsonMapMetaData.Add(newJsonData);
        }
        Globals.SaveMapMetaData(jsonMapMetaData);

        WorldGridManager.Instance.RecycleGrid();
    }
}
