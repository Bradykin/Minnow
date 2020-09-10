using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject m_worldGridLevelCreatorRoot;
    [SerializeField]
    private Image m_selectedImage;
    [SerializeField]
    private Text m_saveFileNotifier;

    private string dataPath;

    private List<string> dataPaths;

    private void Start()
    {
        dataPath = Application.dataPath + "/RemoteData/JsonGridData";
        dataPaths = new List<string>();
        for (int i = 0; i < 6; i++)
        {
            dataPaths.Add(dataPath + i + ".txt");
        }
        Globals.m_currentlyPaintingTerrain = GameTerrainFactory.GetCurrentTerrain();
        m_selectedImage.sprite = Globals.m_currentlyPaintingTerrain.m_icon;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Globals.m_currentlyPaintingType == typeof(GameTerrainBase))
            {
                Globals.m_currentlyPaintingTerrain = GameTerrainFactory.GetNextTerrainList();
                m_selectedImage.sprite = Globals.m_currentlyPaintingTerrain.m_icon;
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
            }

            /*else if (Globals.m_currentlyPaintingType == typeof(GameBuildingBase))
            {
                Globals.m_currentlyPaintingBuilding = GameBuildingFactory.GetNextBuilding(Globals.m_currentlyPaintingBuilding);
                m_selectedImage.sprite = Globals.m_currentlyPaintingTerrain.m_icon;
            }*/
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
        WorldGridManager.Instance.SetupEmptyGrid(transform, Constants.GridSizeX, Constants.GridSizeY);
    }

    public void LoadGrid(int pathIndex)
    {
        m_saveFileNotifier.text = "Save File: " + pathIndex;
        if (!File.Exists(dataPaths[pathIndex]))
        {
            SpawnGrid();
            return;
        }

        JsonGridData jsonData = JsonUtility.FromJson<JsonGridData>(File.ReadAllText(dataPaths[pathIndex]));
        WorldGridManager.Instance.LoadFromJson(jsonData);
        WorldGridManager.Instance.Setup(transform);
    }

    public void SaveGrid(int pathIndex)
    {
        string jsonGridData = WorldGridManager.Instance.SaveToJson();
        File.WriteAllText(dataPaths[pathIndex], jsonGridData);

        WorldGridManager.Instance.RecycleGrid();
    }
}
