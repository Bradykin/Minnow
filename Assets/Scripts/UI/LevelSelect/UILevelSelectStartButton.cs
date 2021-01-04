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
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        if (UILevelSelectController.Instance.m_curMap == null)
        {
            return;
        }

        UICameraController.Instance.m_levelSelectReturnTransform = UICameraController.Instance.transform.position;

        List<JsonMapMetaData> mapList = Globals.LoadMapMetaData();
        for (int i = 0; i < mapList.Count; i++)
        {
            if (mapList[i].mapID == UILevelSelectController.Instance.m_curMap.m_id)
            {
                Globals.mapToLoad = mapList[i].dataPath;
                WorldController.Instance.BeginLevel(UILevelSelectController.Instance.m_curMap, false);
                SceneLoader.ActivateScene("LevelScene", "LevelSelectScene");
                return;
            }
        }
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
