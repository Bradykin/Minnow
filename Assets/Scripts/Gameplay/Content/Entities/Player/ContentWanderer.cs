using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWanderer : GameEntity
{
    private int m_goldGain;

    public ContentWanderer()
    {
        m_goldGain = 25;
        
        m_maxHealth = 4;
        m_maxAP = 4;
        m_apRegen = 4;
        m_power = 3;

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Wanderer";
        m_desc = "When summoned, gain " + m_goldGain + " gold.";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override void OnSummon()
    {
        base.OnSummon();

        GameHelper.GetPlayer().m_wallet.AddResources(new GameWallet(m_goldGain));
    }
}
