using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLootingsCard : GameCardSpellBase
{
    public ContentLootingsCard()
    {
        m_name = "Lootings";
        m_desc = "Loot your own castle, gaining +75 gold but dealing 10 damage to the castle.";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Common;
        m_shouldExile = true;

        SetupBasicData();
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GameHelper.GetPlayer().Castle.GetHit(10);
        GameHelper.GetPlayer().m_wallet.AddResources(new GameWallet(75));
    }
}
