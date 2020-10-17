using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRelic : UIElementBase
    , IPointerClickHandler
{
    public enum RelicSelectionType
    {
        View,
        Select,
        SelectStarter
    }

    private GameRelic m_relic;
    private RelicSelectionType m_selectionType;

    public Image m_image;
    public Image m_rarityTint;

    void Update()
    {
        if (m_selectionType == RelicSelectionType.SelectStarter)
        {
            if (GamePlayer.StarterRelic.m_name == m_relic.m_name)
            {
                m_tintImage.color = UIHelper.GetSelectTintColor(true);
            }
            else
            {
                m_tintImage.color = UIHelper.GetDefaultTintColor();
            }
        }
    }

    public void Init(GameRelic newRelic, RelicSelectionType selectionType)
    {
        m_stopScrolling = true;

        m_relic = newRelic;
        m_selectionType = selectionType;

        m_image.sprite = m_relic.m_icon;
        m_rarityTint.color = UIHelper.GetRarityColor(m_relic.m_rarity);
    }

    public override void HandleTooltip()
    {
        if (m_selectionType == RelicSelectionType.View || m_selectionType == RelicSelectionType.SelectStarter)
        {
            UIHelper.CreateRelicTooltip(m_relic);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        if (m_selectionType == RelicSelectionType.Select)
        {
            UIRelicSelectController.Instance.AcceptRelic(m_relic);
            UITooltipController.Instance.ClearTooltipStack();
        }

        if (m_selectionType == RelicSelectionType.SelectStarter)
        {
            GamePlayer.StarterRelic = m_relic;
        }
    }
}
