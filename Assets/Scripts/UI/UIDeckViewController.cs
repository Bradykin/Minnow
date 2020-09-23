using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UIDeckViewController : Singleton<UIDeckViewController>
{
    public enum DeckViewType
    {
        View,
        Remove,
        Transform,
        Duplicate
    }

    public UICard[] m_cards;

    public GameObject m_holder;
    public int m_index;

    private List<GameCard> m_deck;
    private DeckViewType m_viewType;

    void Update()
    {
        m_holder.SetActive(Globals.m_inDeckView);
    }

    public void Init(List<GameCard> deck, DeckViewType viewType)
    {
        m_deck = deck;
        m_viewType = viewType;

        SetIndex(0);
    }

    public bool CanIndexIncrease()
    {
        if (m_deck == null)
        {
            return false;
        }

        if ((m_index+1) * m_cards.Length < m_deck.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetIndex(int index)
    {
        m_index = index;

        UpdateDeck(m_viewType);
    }

    public void UpdateDeck(DeckViewType newViewType)
    {
        m_viewType = newViewType;
        UITooltipController.Instance.ClearTooltipStack();
        Globals.m_canScroll = false;

        m_holder.SetActive(true);
        Globals.m_inDeckView = true;

        int indexMod = m_index * m_cards.Length;

        for (int i = 0; i < m_cards.Length; i++)
        {
            if (m_deck.Count > i + indexMod)
            {
                m_cards[i].Init(m_deck[i + indexMod], UICard.CardDisplayType.Deck);
                m_cards[i].InitCardDeck(m_viewType);
                m_cards[i].gameObject.SetActive(true);
            }
            else
            {
                m_cards[i].gameObject.SetActive(false);
            }
        }
    }
}
