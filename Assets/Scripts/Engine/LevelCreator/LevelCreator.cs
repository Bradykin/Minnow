using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Game.Util;

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
        for (int i = 0; i < 6; i++)
        {
            dataPaths.Add(dataPath + i + ".txt");
        }
        Globals.m_currentlyPaintingType = typeof(GameTerrainBase);
        Globals.m_currentlyPaintingBuilding = new ContentCastleBuilding();
        Globals.m_currentlyPaintingTerrain = GameTerrainFactory.GetCurrentTerrain();
        Globals.m_currentlyPaintingEvent = new ContentAngelicGiftEvent(null);

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

            /*else if (Globals.m_currentlyPaintingType == typeof(GameBuildingBase))
            {
                Globals.m_currentlyPaintingType = typeof(GameEvent);
                m_selectedImage.sprite = Globals.m_currentlyPaintingEvent.m_icon;
            }
            else if (Globals.m_currentlyPaintingType == typeof(GameEvent))
            {
                Globals.m_currentlyPaintingType = typeof(GameTerrainBase);
                m_selectedImage.sprite = Globals.m_currentlyPaintingTerrain.m_icon;
            }*/
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
                m_selectedTileNotifier.text = "Random spawn point";
            }
            else if (Globals.m_currentlyPaintingType == typeof(GameSpawnPoint))
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UICameraController.Instance.Reset();
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

        JsonGridData jsonData = JsonUtility.FromJson<JsonGridData>(File.ReadAllText(path));

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
