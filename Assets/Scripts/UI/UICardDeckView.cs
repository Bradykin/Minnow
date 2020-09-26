using System.Collections;
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

        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        if (m_viewType == UIDeckViewController.DeckViewType.Remove)
        {
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
            GameCard dupCard = GameCardFactory.GetCardDup(m_uiCard.m_card);
            player.m_deckBase.AddCard(dupCard);
            player.m_curDeck.AddToDiscard(dupCard);
        }
        else if (m_viewType == UIDeckViewController.DeckViewType.Transform)
        {
            GameCard newCard;

            if (m_uiCard.m_card is GameCardEntityBase)
            {
                newCard = GameCardFactory.GetRandomStandardEntityCard();
            }
            else if (m_uiCard.m_card is GameCardSpellBase)
            {
                newCard = GameCardFactory.GetRandomStandardSpellCard();
            }
            else
            {
                newCard = GameCardFactory.GetRandomStandardCard();
            }

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

        UIDeckViewController.Instance.Init(GameHelper.GetPlayer().m_deckBase.GetCardsForRead(), UIDeckViewController.DeckViewType.View, "Base Deck");
    }
}
