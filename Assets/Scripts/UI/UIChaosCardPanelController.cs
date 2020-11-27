using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using TMPro;

public class UIChaosCardPanelController : Singleton<UIChaosCardPanelController>
{
    public GameObject m_holder;

    public UICard m_firstCard;
    public UICard m_secondCard;
    public UICard m_thirdCard;
    public UICard m_fourthCard;

    void Start()
    {
        m_holder.SetActive(false);
    }

    public void Init(GameCard cardOne, GameCard cardTwo, GameCard cardThree, GameCard cardFour)
    {
        m_firstCard.Init(cardOne, UICard.CardDisplayType.Deck);
        m_secondCard.Init(cardTwo, UICard.CardDisplayType.Deck);
        m_thirdCard.Init(cardThree, UICard.CardDisplayType.Deck);
        m_fourthCard.Init(cardFour, UICard.CardDisplayType.Deck);

        m_holder.SetActive(true);
    }

    public void ClosePanel()
    {
        m_holder.SetActive(false);
    }
}
