﻿using System.Collections;
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
        UIStarterCardSelectionController.Instance.SetStarterCard(m_cardType, m_uiCard.m_card);
    }

    public bool IsSelected()
    {
        bool isSelected = false;

        if (m_cardType == UIStarterCardSelectionController.StarterCardType.BasicUnit)
        {
            isSelected = GamePlayer.StarterSimpleUnit.m_name == m_uiCard.m_card.m_name;
        }
        else if (m_cardType == UIStarterCardSelectionController.StarterCardType.AdvancedUnit)
        {
            isSelected = GamePlayer.StarterAdvancedUnit.m_name == m_uiCard.m_card.m_name;
        }
        else if (m_cardType == UIStarterCardSelectionController.StarterCardType.DamageSpell)
        {
            isSelected = GamePlayer.StarterDamageSpell.m_name == m_uiCard.m_card.m_name;
        }
        else if (m_cardType == UIStarterCardSelectionController.StarterCardType.DefensiveSpell)
        {
            isSelected = GamePlayer.StarterDefensiveSpell.m_name == m_uiCard.m_card.m_name;
        }
        else if (m_cardType == UIStarterCardSelectionController.StarterCardType.ExileSpell)
        {
            isSelected = GamePlayer.StarterExileSpell.m_name == m_uiCard.m_card.m_name;
        }

        return isSelected;
    }
}