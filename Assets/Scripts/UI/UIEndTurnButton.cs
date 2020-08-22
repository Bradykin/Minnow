using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndTurnButton : MonoBehaviour
{
    public SpriteRenderer m_tintRenderer;

    void OnMouseDown()
    {
        Globals.m_selectedCard = null;
        Globals.m_selectedEntity = null;

        WorldController.Instance.EndTurn();

        UIHelper.SetDefaultTintColor(m_tintRenderer);
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
