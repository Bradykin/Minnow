using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //Start the level builder;
        }
        else
        {
            JsonMapMetaData m_curLevel = UILevelSelectController.Instance.m_curLevel;
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
