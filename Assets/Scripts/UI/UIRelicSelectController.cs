using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;
using TMPro;

public class UIRelicSelectController : Singleton<UIRelicSelectController>
{
    public GameObject m_holder;

    public UIRelic m_firstButton;
    public UIRelic m_secondButton;

    public TMP_Text m_skipText;

    private GameWallet m_skipWallet;

    void Start()
    {
        m_holder.SetActive(false);
    }

    public void Init(GameRelic relicOne, GameRelic relicTwo)
    {
        GameHelper.GetGameController().AddIntermissionLock();
        
        m_skipWallet = new GameWallet(25);

        Globals.m_canSelect = false;

        m_firstButton.gameObject.SetActive(true);
        m_firstButton.Init(relicOne, UIRelic.RelicSelectionType.Select);

        m_secondButton.gameObject.SetActive(true);
        m_secondButton.Init(relicTwo, UIRelic.RelicSelectionType.Select);

        m_skipText.text = "Skip: +" + m_skipWallet.m_gold + " gold.";

        m_holder.SetActive(true);
    }

    public void EndSelection()
    {
        Globals.m_canSelect = true;
        m_holder.SetActive(false);

        GameHelper.GetGameController().RemoveIntermissionLock();
    }

    public void AcceptRelic(GameRelic relic)
    {
        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        GameNotificationManager.RecordRelicChoice(relic, m_firstButton.m_relic, m_secondButton.m_relic);

        player.AddRelic(relic);

        EndSelection();
    }

    public void SkipSelection()
    {
        GameHelper.GetPlayer().GainGold(m_skipWallet.m_gold);

        EndSelection();
    }
}
