using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRelicSelectSkipButton : MonoBehaviour
{
    public Image m_tintImage;

    void OnMouseDown()
    {
        UIRelicSelectController.Instance.SkipSelection();
        m_tintImage.color = UIHelper.GetDefaultTintColor();
        Globals.m_selectedCard = null;
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
