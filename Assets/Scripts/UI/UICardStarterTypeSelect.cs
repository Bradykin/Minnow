using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICardStarterTypeSelect : MonoBehaviour
    , IPointerClickHandler
{
    private UIStarterCardSelectionController.StarterCardType m_cardType;

    private UICard m_uiCard;

    public void Init(UIStarterCardSelectionController.StarterCardType cardType)
    {
        m_cardType = cardType;
        m_uiCard = GetComponent<UICard>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_cardType == UIStarterCardSelectionController.Instance.m_curSelectedType)
        {
            UIStarterCardSelectionController.Instance.SetCurSelectedType(UIStarterCardSelectionController.StarterCardType.None);
            Globals.m_selectedCard = null;
            return;
        }
        else
        {
            UIStarterCardSelectionController.Instance.SetCurSelectedType(m_cardType);
            Globals.m_selectedCard = m_uiCard;
        }
    }
}
