using System.Collections;
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

    private void Start()
    {
        dataPath = "JsonGridData";
        dataPaths = new List<string>();
        for (int i = 0; i < 10; i++)
        {
            dataPaths.Add(dataPath + i + ".txt");
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
                m_selectedTileNotifier.text = Globals.m_currentlyPaintingTerrain.m_name;
            }
            else if (Globals.m_currentlyPaintingType == typeof(GameBuildingBase))
            {
                Globals.m_currentlyPaintingBuilding = GameBuildingFactory.GetNextBuilding(Globals.m_currentlyPaintingBuilding);
                m_selectedImage.sprite = Globals.m_currentlyPaintingBuilding.m_icon;
                m_selectedTileNotifier.text = Globals.m_currentlyPaintingBuilding.m_name;
            }
            else if (Globals.m_currentlyPaintingType == typeof(GameSpawnPoint))
            {
                Globals.m_currentlyPaintingNumberIndex++;
                if (Globals.m_currentlyPaintingNumberIndex > 5)
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
                if (Globals.m_currentlyPaintingNumberIndex > 5)
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

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Globals.m_currentlyPaintingType == typeof(GameTerrainBase))
            {
                Globals.m_currentlyPaintingType = typeof(GameBuildingBase);
                m_selectedImage.sprite = Globals.m_currentlyPaintingBuilding.m_icon;
                m_selectedListNotifier.text = "Buildings";
                m_selectedTileNotifier.text = Globals.m_currentlyPaintingBuilding.m_name;
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ProcessSaveFileKey(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ProcessSaveFileKey(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ProcessSaveFileKey(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ProcessSaveFileKey(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ProcessSaveFileKey(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ProcessSaveFileKey(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ProcessSaveFileKey(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ProcessSaveFileKey(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ProcessSaveFileKey(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ProcessSaveFileKey(9);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneLoader.ActivateScene("LevelSelectScene", "LevelCreatorScene");
        }
    }

    public void ProcessSaveFileKey(int pathIndex)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            SaveGrid(pathIndex);
        }
        else
        {
            LoadGrid(pathIndex);
        }
    }

    public void SpawnGrid()
    {
        WorldGridManager.Instance.SetupEmptyGrid(transform, Globals.GridSizeX, Globals.GridSizeY);
    }

    public void LoadGrid(int pathIndex)
    {
        m_saveFileNotifier.text = "Save File: " + (pathIndex + 1);

#if UNITY_EDITOR
        string path = Path.Combine(GameFiles.EDITOR_PATH, dataPaths[pathIndex]);
#else
        string path = Path.Combine(GameFiles.BUILD_PATH, dataPaths[pathIndex]);
#endif

        if (!File.Exists(path))
        {
            SpawnGrid();
            return;
        }

        JsonGridData jsonData = JsonConvert.DeserializeObject<JsonGridData>(File.ReadAllText(path));

        WorldGridManager.Instance.LoadFromJson(jsonData);
        WorldGridManager.Instance.Setup(m_worldGridLevelCreatorRoot.transform);
    }

    public void SaveGrid(int pathIndex)
    {
        string jsonGridData = WorldGridManager.Instance.SaveToJsonAsString();
#if UNITY_EDITOR
        File.WriteAllText(Path.Combine(GameFiles.EDITOR_PATH, dataPaths[pathIndex]), jsonGridData);
#else
        File.WriteAllText(Path.Combine(GameFiles.BUILD_PATH, dataPaths[pathIndex]), jsonGridData);
#endif

        List<JsonMapMetaData> jsonMapMetaData = Globals.LoadMapMetaData();
        bool containsMapData = jsonMapMetaData.Any(m => m.dataPath == dataPaths[pathIndex]);

        JsonMapMetaData newJsonData = new JsonMapMetaData
        {
            mapName = dataPaths[pathIndex],
            mapID = pathIndex,
            gridSize = new Vector2Int(Globals.GridSizeX, Globals.GridSizeY),
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
