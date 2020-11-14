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
        SelectStarter,
        ViewNoTooltip
    }

    private GameRelic m_relic;
    private RelicSelectionType m_selectionType;

    public Image m_image;
    public Image m_rarityTint;

    public bool m_isLocked;

    void Update()
    {
        if (m_selectionType == RelicSelectionType.SelectStarter)
        {
            if (PlayerDataManager.PlayerAccountData.StarterRelicName == m_relic.GetName())
            {
                m_tintImage.color = UIHelper.GetSelectTintColor(true);
            }
            else
            {
                if (m_isShowingTooltip)
                {
                    m_tintImage.color = UIHelper.GetValidTintColor(true);
                }
                else
                {
                    m_tintImage.color = UIHelper.GetDefaultTintColor();
                }
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

        if (m_relic.m_rarity == GameElementBase.GameRarity.Starter)
        {
            m_isLocked = GameMetaprogressionUnlocksDataManager.HasUnlocked(m_relic);
        }
        else
        {
            m_isLocked = false;
        }
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
            AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);
        }

        if (m_selectionType == RelicSelectionType.SelectStarter)
        {
            PlayerDataManager.PlayerAccountData.StarterRelicName = m_relic.GetBaseName();
            AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);
        }
    }
}
