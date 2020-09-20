using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICloseDeckViewButton : MonoBehaviour
{
    public Image m_tintImage;

    void OnMouseDown()
    {
        Globals.m_inDeckView = false;
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
