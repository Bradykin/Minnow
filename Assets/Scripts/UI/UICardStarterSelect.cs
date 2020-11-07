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
        UIStarterCardSelectionController.Instance.SetStarterCard(m_cardType, m_uiCard.m_card);
        
        AudioHelper.PlaySFX(AudioHelper.UICardClick);
    }

    public bool IsSelected()
    {
        bool isSelected = false;

        if (m_cardType == UIStarterCardSelectionController.StarterCardType.BasicUnit)
        {
            isSelected = PlayerDataManager.PlayerAccountData.StarterSimpleUnitName == m_uiCard.m_card.GetName();
        }
        else if (m_cardType == UIStarterCardSelectionController.StarterCardType.AdvancedUnit)
        {
            isSelected = PlayerDataManager.PlayerAccountData.StarterAdvancedUnitName == m_uiCard.m_card.GetName();
        }
        else if (m_cardType == UIStarterCardSelectionController.StarterCardType.DamageSpell)
        {
            isSelected = PlayerDataManager.PlayerAccountData.StarterDamageSpellName == m_uiCard.m_card.GetName();
        }
        else if (m_cardType == UIStarterCardSelectionController.StarterCardType.DefensiveSpell)
        {
            isSelected = PlayerDataManager.PlayerAccountData.StarterDefensiveSpellName == m_uiCard.m_card.GetName();
        }
        else if (m_cardType == UIStarterCardSelectionController.StarterCardType.ExileSpell)
        {
            isSelected = PlayerDataManager.PlayerAccountData.StarterExileSpellName == m_uiCard.m_card.GetName();
        }

        return isSelected;
    }
}
