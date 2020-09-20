using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelSelectStartButton : MonoBehaviour
{
    public Image m_tintImage;

    void OnMouseDown()
    {
        StartGame();
    }

    private void StartGame()
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

    void OnMouseOver()
    {
        m_tintImage.color = UIHelper.GetValidTintColor(true);
    }

    void OnMouseExit()
    {
        m_tintImage.color = UIHelper.GetDefaultTintColor();
    }
}
