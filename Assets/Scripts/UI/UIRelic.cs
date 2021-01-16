using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIRelic : UIElementBase
    , IPointerClickHandler
{
    public enum RelicSelectionType
    {
        View,
        Select,
        ViewNoTooltip
    }

    public GameRelic m_relic;
    private RelicSelectionType m_selectionType;
    public UIRelicPickupAnimation m_pickupAnimation;
    public UIRelicTriggerAnimation m_triggerAnimation;

    public Image m_image;
    public Image m_rarityFrame;

    public bool m_usesText = false;
    public TMP_Text m_titleText;
    public TMP_Text m_descText;

    public void Init(GameRelic newRelic, RelicSelectionType selectionType)
    {
        m_stopScrolling = true;

        m_relic = newRelic;
        m_selectionType = selectionType;

        m_image.sprite = m_relic.m_icon;
        m_rarityFrame.sprite = UIHelper.GetRelicRarityFrame(m_relic.m_rarity);

        if (m_usesText)
        {
            m_titleText.gameObject.SetActive(true);
            m_descText.gameObject.SetActive(true);

            m_titleText.text = m_relic.GetName();
            m_descText.text = m_relic.GetDesc();
        }
        else
        {
            m_titleText.text = "";
            m_descText.text = "";

            m_titleText.gameObject.SetActive(false);
            m_descText.gameObject.SetActive(false);

            m_pickupAnimation.PlayAnim();
        }
    }

    public void OnTrigger()
    {
        m_triggerAnimation.PlayAnim();
    }

    public override void HandleTooltip()
    {
        if (m_selectionType == RelicSelectionType.View)
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
    }
}
