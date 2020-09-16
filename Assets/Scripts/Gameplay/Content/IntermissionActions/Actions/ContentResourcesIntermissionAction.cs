using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentResourcesIntermissionAction : GameActionIntermission
{
    private GameWallet m_wallet = new GameWallet(35);

    public ContentResourcesIntermissionAction()
    {
        m_actionCost = 1;
        m_name = "Gather Resources";
        m_desc = "Gain " + m_wallet.m_gold + " gold.";

        m_icon = UIHelper.GetIconIntermissionAction(m_name);
    }

    public override void Activate()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.m_wallet.AddResources(m_wallet);

        SpendCost();
    }
}
