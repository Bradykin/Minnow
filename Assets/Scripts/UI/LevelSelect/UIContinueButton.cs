using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.Util;

public class UIContinueButton : UIElementBase
        , IPointerClickHandler
{
    public GameObject m_holdler;

    void Update()
    {
        m_holdler.SetActive(PlayerDataManager.PlayerAccountData.PlayerRunData != null);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        if (m_holdler.activeSelf)
        {
            LoadSavedRun();
        }
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }

    public void LoadSavedRun()
    {
        if (PlayerDataManager.PlayerAccountData.PlayerRunData == null)
        {
            return;
        }

        List<JsonMapMetaData> mapList = Globals.LoadMapMetaData();
        int mapId = PlayerDataManager.PlayerAccountData.PlayerRunData.m_jsonGameControllerData.mapId;

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
}
