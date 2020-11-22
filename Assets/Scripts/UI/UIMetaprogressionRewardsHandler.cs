using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMetaprogressionRewardsHandler : MonoBehaviour
{
    public GameObject m_holder;
    public GameObject m_moreIndicator;

    public TMP_Text m_unlockTitle;
    public TMP_Text m_unlockDesc;

    public UICard m_card1;
    public UICard m_card2;
    public UICard m_card3;

    public UIRelic m_relic1;
    public UIRelic m_relic2;
    public UIRelic m_relic3;

    public Image m_map;

    public UICard m_starterCard;

    public UIRelic m_starterRelic;

    void Update()
    {
        bool shouldShow = UIMetaprogressionNotificationController.HasAnyNotifications();

        m_holder.SetActive(shouldShow);

        if (!shouldShow)
        {
            return;
        }

        m_moreIndicator.SetActive(UIMetaprogressionNotificationController.HasMultipleNotifications());

        GameMetaprogressionReward reward = UIMetaprogressionNotificationController.GetReward();

        m_unlockTitle.text = reward.GetTitle();
        m_unlockDesc.text = reward.GetDesc();

        if (reward.IsCards())
        {
            UnlockCards(reward.GetCards());
        }
        else if (reward.IsRelics())
        {
            UnlockRelics(reward.GetRelics());
        }
        else if (reward.IsMap())
        {
            UnlockMap(reward.GetMap());
        }
        else if (reward.IsStarterCard())
        {
            UnlockStarterCard(reward.GetStarterCard());
        }
        else if (reward.IsStarterRelic())
        {
            UnlockStarterRelic(reward.GetStarterRelic());
        }
    }

    private void UnlockCards(List<GameCard> cards)
    {
        SetCardVisibility(true);

        SetRelicVisibility(false);
        SetMapVisibility(false);
        SetStarterCardVisibility(false);
        SetStarterRelicVisibility(false);

        m_card1.Init(cards[0], UICard.CardDisplayType.RewardDisplay);
        m_card2.Init(cards[1], UICard.CardDisplayType.RewardDisplay);
        m_card3.Init(cards[2], UICard.CardDisplayType.RewardDisplay);
    }

    private void UnlockRelics(List<GameRelic> relics)
    {
        SetRelicVisibility(true);

        SetCardVisibility(false);
        SetMapVisibility(false);
        SetStarterCardVisibility(false);
        SetStarterRelicVisibility(false);

        m_relic1.Init(relics[0], UIRelic.RelicSelectionType.ViewNoTooltip);
        m_relic2.Init(relics[1], UIRelic.RelicSelectionType.ViewNoTooltip);
        m_relic3.Init(relics[2], UIRelic.RelicSelectionType.ViewNoTooltip);
    }

    private void UnlockMap(GameMap map)
    {
        SetMapVisibility(true);

        SetCardVisibility(false);
        SetRelicVisibility(false);
        SetStarterCardVisibility(false);
        SetStarterRelicVisibility(false);

        m_map.sprite = map.m_icon;
    }

    private void UnlockStarterCard(GameCard card)
    {
        SetStarterCardVisibility(true);

        SetMapVisibility(false);
        SetCardVisibility(false);
        SetRelicVisibility(false);
        SetStarterRelicVisibility(false);

        m_starterCard.Init(card, UICard.CardDisplayType.RewardDisplay);
    }

    private void UnlockStarterRelic(GameRelic relic)
    {
        SetStarterRelicVisibility(true);

        SetMapVisibility(false);
        SetCardVisibility(false);
        SetRelicVisibility(false);
        SetStarterCardVisibility(false);

        m_starterRelic.Init(relic, UIRelic.RelicSelectionType.ViewNoTooltip);
    }

    private void SetCardVisibility(bool visible)
    {
        m_card1.gameObject.SetActive(visible);
        m_card2.gameObject.SetActive(visible);
        m_card3.gameObject.SetActive(visible);
    }

    private void SetRelicVisibility(bool visible)
    {
        m_relic1.gameObject.SetActive(visible);
        m_relic2.gameObject.SetActive(visible);
        m_relic3.gameObject.SetActive(visible);
    }

    private void SetMapVisibility(bool visible)
    {
        m_map.gameObject.SetActive(visible);
    }

    private void SetStarterCardVisibility(bool visible)
    {
        m_starterCard.gameObject.SetActive(visible);
    }

    private void SetStarterRelicVisibility(bool visible)
    {
        m_starterRelic.gameObject.SetActive(visible);
    }
}
