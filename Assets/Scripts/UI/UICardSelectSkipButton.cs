using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICardSelectSkipButton : MonoBehaviour
{
    public SpriteRenderer m_tintRenderer;

    void OnMouseDown()
    {
        UICardSelectController.Instance.SkipSelection();
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
