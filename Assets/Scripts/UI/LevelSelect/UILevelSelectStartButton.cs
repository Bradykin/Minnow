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
        //Start the game here
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
