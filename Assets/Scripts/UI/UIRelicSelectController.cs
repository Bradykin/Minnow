using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;

public class UIRelicSelectController : Singleton<UIRelicSelectController>
{
    public GameObject m_holder;

    public UIRelic m_firstButton;
    public UIRelic m_secondButton;

    public Text m_firstRelicText;
    public Text m_firstRelicDescText;
    public Text m_secondRelicText;
    public Text m_secondRelicDescText;

    public Text m_skipText;

    private GameWallet m_skipWallet;

    void Start()
    {
        m_holder.SetActive(false);
    }

    public void Init(GameRelic relicOne, GameRelic relicTwo)
    {
        m_skipWallet = new GameWallet(25);

        Globals.m_canSelect = false;

        m_firstButton.gameObject.SetActive(true);
        m_firstButton.Init(relicOne, UIRelic.RelicSelectionType.Select);
        m_firstRelicText.text = relicOne.GetName();
        m_firstRelicDescText.text = relicOne.GetDesc();

        m_secondButton.gameObject.SetActive(true);
        m_secondButton.Init(relicTwo, UIRelic.RelicSelectionType.Select);
        m_secondRelicText.text = relicTwo.GetName();
        m_secondRelicDescText.text = relicTwo.GetDesc();

        m_skipText.text = "Skip: +" + m_skipWallet.m_gold + " gold.";

        m_holder.SetActive(true);
    }

    public void EndSelection()
    {
        Globals.m_canSelect = true;
        m_holder.SetActive(false);
    }

    public void AcceptRelic(GameRelic relic)
    {
        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        player.AddRelic(relic);

        EndSelection();
    }

    public void SkipSelection()
    {
        GameHelper.GetPlayer().m_wallet.AddResources(m_skipWallet);

        EndSelection();
    }
}
