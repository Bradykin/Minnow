using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UILevelSelectStartButton : UIElementBase
    , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        List<JsonMapMetaData> mapList = Globals.LoadMapMetaData();
        for (int i = 0; i < mapList.Count; i++)
        {
            if (mapList[i].mapID == UILevelSelectController.Instance.m_curMap.m_id)
            {
                Globals.mapToLoad = mapList[i].dataPath;
                SceneLoader.ActivateScene("LevelScene", "LevelSelectScene");
            }
        }
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
