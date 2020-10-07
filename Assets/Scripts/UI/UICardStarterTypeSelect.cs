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
            UIStarterCardSelectionController.Instance.m_curSelectedType = UIStarterCardSelectionController.StarterCardType.None;
            return;
        }
        else
        {
            UIStarterCardSelectionController.Instance.m_curSelectedType = m_cardType;
        }
    }
}
