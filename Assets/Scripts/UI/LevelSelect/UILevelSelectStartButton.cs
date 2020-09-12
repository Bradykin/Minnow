using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevelSelectStartButton : MonoBehaviour
{
    public SpriteRenderer m_tintRenderer;

    void OnMouseDown()
    {
        StartGame();
    }

    private void StartGame()
    {
        if (UILevelSelectController.Instance.m_levelBuilderSelected)
        {
            SceneLoader.ActivateScene("LevelCreatorScene", "LevelSelectScene");
            //Start the level builder;
        }
        else
        {
            JsonMapMetaData m_curLevel = UILevelSelectController.Instance.m_curLevel;
            Globals.mapToLoad = m_curLevel.dataPath;
            SceneLoader.ActivateScene("LevelScene", "LevelSelectScene");
            //Load the above level
        }

    }

    void OnMouseOver()
    {
        UIHelper.SetValidTintColor(m_tintRenderer, true);
    }

    void OnMouseExit()
    {
        UIHelper.SetDefaultTintColor(m_tintRenderer);
    }
}
