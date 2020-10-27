using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;

public class UILevelSelectController : Singleton<UILevelSelectController>, IReset
{
    public GameMap m_curMap;

    public Text m_nameText;
    public Text m_descText;
    public GameObject m_infoObj;
    public Text m_difficultyText;
    public Text m_chaosVal;
    public Text m_chaosText;
    public Text m_chaosTitleText;

    public GameObject m_startGameButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            LoadSavedRun();
        }
        
        if (m_curMap == null)
        {
            m_infoObj.SetActive(false);

            m_chaosVal.text = "";
            m_chaosText.text = "";
            m_chaosTitleText.text = "";

            m_nameText.text = "Select a Level";
            m_difficultyText.text = "";

            m_startGameButton.SetActive(false);
        }
        else
        {
            m_infoObj.SetActive(true);

            m_nameText.text = m_curMap.m_name;
            m_descText.text = m_curMap.GetDesc();
            m_difficultyText.text = UIHelper.GetDifficultyText(m_curMap.m_difficulty);
            m_difficultyText.color = UIHelper.GetDifficultyTextColor(m_curMap.m_difficulty);

            if (m_curMap.m_difficulty == MapDifficulty.Introduction)
            {
                m_chaosTitleText.gameObject.SetActive(false);
                m_chaosVal.gameObject.SetActive(false);
                m_chaosText.gameObject.SetActive(false);
            }
            else
            {
                m_chaosTitleText.gameObject.SetActive(true);
                m_chaosVal.gameObject.SetActive(true);
                m_chaosText.gameObject.SetActive(true);

                m_chaosTitleText.text = "Chaos";
                m_chaosVal.text = "" + Globals.m_curChaos;
                m_chaosText.text = UIHelper.GetChaosDesc(Globals.m_curChaos);
            }          

            m_startGameButton.SetActive(true);
        }
    }

    public bool HasLevelSelected()
    {
        return m_curMap != null;
    }

    public void LoadSavedRun()
    {
        if (PlayerDataManager.PlayerAccountData.PlayerRunData == null)
        {
            return;
        }
        
        List<JsonMapMetaData> mapList = Globals.LoadMapMetaData();
        int mapId = PlayerDataManager.PlayerAccountData.PlayerRunData.m_mapId;

        for (int i = 0; i < mapList.Count; i++)
        {
            if (mapList[i].mapID == mapId)
            {
                Globals.mapToLoad = mapList[i].dataPath;
                Globals.loadingRun = true;
                WorldController.Instance.BeginLevel(GameMapFactory.GetMapById(mapId));
                SceneLoader.ActivateScene("LevelScene", "LevelSelectScene");
                return;
            }
        }
    }

    public void SetSelectedLevel(GameMap newMap)
    {
        m_curMap = newMap;
    }

    public void Activate()
    {
        UICameraController.Instance.transform.position = UICameraController.Instance.m_startingTransform;
    }

    public void Reset()
    {
        
    }
}
