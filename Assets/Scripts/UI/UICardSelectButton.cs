using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardSelectButton : MonoBehaviour
{
    private UICard m_uiCard;

    void Start()
    {
        m_uiCard = GetComponent<UICard>();
    }

    void OnMouseDown()
    {
        if (Globals.m_selectedCard == m_uiCard)
        {
            Globals.m_selectedCard = null;
        }
        else
        {
            Globals.m_selectedCard = m_uiCard;
            m_uiCard.m_tintImage.color = UIHelper.GetSelectTintColor(Globals.m_selectedCard == m_uiCard);
        }
    }
}
