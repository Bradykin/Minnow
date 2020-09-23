using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;

public class UIDeckViewController : Singleton<UIDeckViewController>
{
    public enum DeckViewType
    {
        View,
        Remove,
        Transform,
        Duplicate
    }

    public enum DeckFilterType
    {
        All,
        Entities,
        Spells
    }

    public UICard[] m_cards;

    public GameObject m_holder;
    public Text m_deckViewText;
    public int m_index;

    private List<GameCard> m_deck;
    private DeckViewType m_viewType;
    private DeckFilterType m_filterType;
    

    void Update()
    {
        m_holder.SetActive(Globals.m_inDeckView);
    }

    public void Init(List<GameCard> deck, DeckViewType viewType, DeckFilterType filterType, string displayString)
    {
        m_deck = deck;
        m_viewType = viewType;
        m_filterType = filterType;

        m_deckViewText.text = displayString;

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
            /*switch(m_filterType)
            {
                case DeckFilterType.Entities:
                    if (!(m_deck[i + indexMod] is GameCardEntityBase))
                    {
                        continue;
                    }
                    break;
                case DeckFilterType.Spells:
                    if (!(m_deck[i + indexMod] is GameCardSpellBase))
                    {
                        continue;
                    }
                    break;
                case DeckFilterType.All:
                default:
                    break;
            }*/
            
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

        m_filterType = DeckFilterType.All;
    }
}
