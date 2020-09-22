using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UILevelSelectStartButton : WorldElementBase
    , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (UILevelSelectController.Instance.m_levelBuilderSelected)
        {
            SceneLoader.ActivateScene("LevelCreatorScene", "LevelSelectScene");
        }
        else
        {
            JsonMapMetaData m_curLevel = UILevelSelectController.Instance.m_curLevel;
            Globals.mapToLoad = m_curLevel.dataPath;
            SceneLoader.ActivateScene("LevelScene", "LevelSelectScene");
        }
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
