using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class UICardSelectController : Singleton<UICardSelectController>
{
    public GameObject m_holder;

    public UICardSelectButton m_firstButton;
    public UICardSelectButton m_secondButton;
    public UICardSelectButton m_thirdButton;

    void Start()
    {
        m_holder.SetActive(false);
    }

    public void Init(GameCard cardOne, GameCard cardTwo, GameCard cardThree)
    {
        Globals.m_canSelect = false;

        if (cardOne != null)
        {
            m_firstButton.gameObject.SetActive(true);
            m_firstButton.Init(cardOne);
        }
        else
        {
            m_firstButton.gameObject.SetActive(false);
        }

        if (cardTwo != null)
        {
            m_secondButton.gameObject.SetActive(true);
            m_secondButton.Init(cardTwo);
        }
        else
        {
            m_secondButton.gameObject.SetActive(false);
        }

        if (cardThree != null)
        {
            m_thirdButton.gameObject.SetActive(true);
            m_thirdButton.Init(cardThree);
        }
        else
        {
            m_thirdButton.gameObject.SetActive(false);
        }

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

        player.AddCardToDeck(card);

        EndSelection();
    }

    public void SkipSelection()
    {
        EndSelection();
    }
}
