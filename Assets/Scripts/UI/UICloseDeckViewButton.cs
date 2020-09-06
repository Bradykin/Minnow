using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICloseDeckViewButton : MonoBehaviour
{
    public SpriteRenderer m_tintRenderer;

    void OnMouseDown()
    {
        Globals.m_inDeckView = false;
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
