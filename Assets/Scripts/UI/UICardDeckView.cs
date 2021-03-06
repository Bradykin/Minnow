﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICardDeckView : MonoBehaviour
    , IPointerClickHandler
{
    private UIDeckViewController.DeckViewType m_viewType;

    private UICard m_uiCard;

    public void Init(UIDeckViewController.DeckViewType viewType)
    {
        m_viewType = viewType;
        m_uiCard = GetComponent<UICard>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_viewType == UIDeckViewController.DeckViewType.View)
        {
            return;
        }

        AudioHelper.PlaySFX(AudioHelper.UICardClick);

        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        if (m_viewType == UIDeckViewController.DeckViewType.Remove)
        {
            GameNotificationManager.RecordCardRemoval(m_uiCard.m_card);
            player.m_deckBase.RemoveCard(m_uiCard.m_card);

            if (player.m_hand.Contains(m_uiCard.m_card))
            {
                player.m_hand.Remove(m_uiCard.m_card);
            }
            else
            {
                player.m_curDeck.RemoveCard(m_uiCard.m_card);
            }
        }
        else if (m_viewType == UIDeckViewController.DeckViewType.Duplicate)
        {
            GameNotificationManager.RecordCardDuplication(m_uiCard.m_card);
            GameCard dupCard = GameCardFactory.GetCardDup(m_uiCard.m_card);
            player.m_deckBase.AddCard(dupCard);
            player.m_curDeck.AddToDiscard(dupCard);
        }
        else if (m_viewType == UIDeckViewController.DeckViewType.Transform)
        {
            GameCard newCard;

            if (m_uiCard.m_card is GameUnitCard)
            {
                newCard = GameCardFactory.GetRandomStandardUnitCard();
            }
            else if (m_uiCard.m_card is GameCardSpellBase)
            {
                newCard = GameCardFactory.GetRandomStandardSpellCard();
            }
            else
            {
                newCard = GameCardFactory.GetRandomStandardCard();
            }

            GameNotificationManager.RecordCardTransformation(m_uiCard.m_card, newCard);

            if (player.m_hand.Contains(m_uiCard.m_card))
            {
                player.m_hand.Remove(m_uiCard.m_card);
                player.m_hand.Add(newCard);
            }
            else
            {
                player.m_curDeck.AddToDiscard(newCard);
                player.m_curDeck.RemoveCard(m_uiCard.m_card);
            }

            player.m_deckBase.RemoveCard(m_uiCard.m_card);
            player.m_deckBase.AddCard(newCard);
        }
        else if (m_viewType == UIDeckViewController.DeckViewType.Buff)
        {
            ((GameUnitCard)m_uiCard.m_card).GetUnit().AddStats(Constants.IntermissionBuffValue, Constants.IntermissionBuffValue, true, false);
        }

        if (UIDeckViewController.Instance.m_onInteractCallBack != null)
        {
            UIDeckViewController.Instance.m_onInteractCallBack.Invoke();
        }

        UIDeckViewController.Instance.Init(GameHelper.GetPlayer().m_deckBase.GetCardsForRead(), UIDeckViewController.DeckViewType.View, "Base Deck");
    }
}
