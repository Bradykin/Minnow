using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentResourcesIntermissionAction : GameActionIntermission
{
    private GameWallet m_wallet = new GameWallet(50);

    public ContentResourcesIntermissionAction()
    {
        m_name = "Gather Resources";
        m_desc = "Gain " + m_wallet.m_gold + " gold.";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate(Action action)
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.GainGold(m_wallet.m_gold, true);
        action.Invoke();
    }
}
