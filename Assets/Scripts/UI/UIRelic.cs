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
        SelectStarter,
        ViewNoTooltip
    }

    public GameRelic m_relic;
    private RelicSelectionType m_selectionType;
    public UIRelicPickupAnimation m_pickupAnimation;

    public Image m_image;
    public Image m_rarityFrame;

    public bool m_isLocked;
    public GameObject m_lockIcon;

    public bool m_usesText = false;
    public TMP_Text m_titleText;
    public TMP_Text m_descText;

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

        if (m_selectionType == RelicSelectionType.SelectStarter)
        {
            m_isLocked = (!GameMetaprogressionUnlocksDataManager.HasUnlockedStarterRelic(m_relic) && !Constants.UnlockAllContent);
        }
        else
        {
            m_isLocked = false;
        }

        if (m_lockIcon != null)
        {
            m_lockIcon.SetActive(m_isLocked);
        }
    }

    public override void HandleTooltip()
    {
        if (m_isLocked && m_selectionType == RelicSelectionType.SelectStarter)
        {
            GameMap neededMap = GameMetaprogressionUnlocksDataManager.GetMapForStarterRelic(m_relic);
            if (neededMap == null)
            {
                UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("WIP", "Relic not yet available in this version of the game"));
            }
            else
            {
                UITooltipController.Instance.AddTooltipToStack(UIHelper.CreateSimpleTooltip("Locked Relic", "Unlock this relic by beating Chaos 2 on " + neededMap.GetBaseName()));
            }
        }
        else if (m_selectionType == RelicSelectionType.View || m_selectionType == RelicSelectionType.SelectStarter)
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

        if (m_isLocked)
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
