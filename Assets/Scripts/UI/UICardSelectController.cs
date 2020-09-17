using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;

public class UICardSelectController : Singleton<UICardSelectController>
{
    public GameObject m_holder;

    public UICard m_firstButton;
    public UICard m_secondButton;
    public UICard m_thirdButton;

    public Text m_skipText;

    private GameWallet m_skipWallet;

    void Start()
    {
        m_holder.SetActive(false);
    }

    public void Init(GameCard cardOne, GameCard cardTwo, GameCard cardThree)
    {
        Globals.m_canSelect = false;

        m_skipWallet = new GameWallet(10);

        if (cardOne != null)
        {
            m_firstButton.gameObject.SetActive(true);
            m_firstButton.Init(cardOne, UICard.CardDisplayType.Select);
        }
        else
        {
            m_firstButton.gameObject.SetActive(false);
        }

        if (cardTwo != null)
        {
            m_secondButton.gameObject.SetActive(true);
            m_secondButton.Init(cardTwo, UICard.CardDisplayType.Select);
        }
        else
        {
            m_secondButton.gameObject.SetActive(false);
        }

        if (cardThree != null)
        {
            m_thirdButton.gameObject.SetActive(true);
            m_thirdButton.Init(cardThree, UICard.CardDisplayType.Select);
        }
        else
        {
            m_thirdButton.gameObject.SetActive(false);
        }

        m_skipText.text = "Skip: +" + m_skipWallet.m_gold + " gold.";

        m_holder.SetActive(true);
    }

    public void EndSelection()
    {
        Globals.m_canSelect = true;
        m_holder.SetActive(false);
    }

    public void AcceptCard(GameCard card)
    {
        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        player.AddCardToDiscard(card, true);

        EndSelection();
    }

    public void SkipSelection()
    {
        GameHelper.GetPlayer().m_wallet.AddResources(m_skipWallet);

        EndSelection();
    }
}
