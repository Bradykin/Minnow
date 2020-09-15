using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRelicSelectSkipButton : MonoBehaviour
{
    public SpriteRenderer m_tintRenderer;

    void OnMouseDown()
    {
        UIRelicSelectController.Instance.SkipSelection();
        UIHelper.SetDefaultTintColor(m_tintRenderer);
        Globals.m_selectedCard = null;
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
