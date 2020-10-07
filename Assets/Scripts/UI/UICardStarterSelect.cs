using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICardStarterSelect : MonoBehaviour
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
        if (m_cardType == UIStarterCardSelectionController.StarterCardType.BasicUnit)
        {
            GamePlayer.StarterSimpleUnit = m_uiCard.m_card;
        }
        else if (m_cardType == UIStarterCardSelectionController.StarterCardType.AdvancedUnit)
        {
            GamePlayer.StarterAdvancedUnit = m_uiCard.m_card;
        }
        else if (m_cardType == UIStarterCardSelectionController.StarterCardType.DamageSpell)
        {
            GamePlayer.StarterDamageSpell = m_uiCard.m_card;
        }
        else if (m_cardType == UIStarterCardSelectionController.StarterCardType.DefensiveSpell)
        {
            GamePlayer.StarterDefensiveSpell = m_uiCard.m_card;
        }
        else if (m_cardType == UIStarterCardSelectionController.StarterCardType.ExileSpell)
        {
            GamePlayer.StarterExileSpell = m_uiCard.m_card;
        }
    }
}
