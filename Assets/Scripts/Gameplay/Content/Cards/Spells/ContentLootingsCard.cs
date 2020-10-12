using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLootingsCard : GameCardSpellBase
{
    private int m_lootVal = 75;
    private int m_damageVal = 10;

    public ContentLootingsCard()
    {
        m_name = "Lootings";
        m_desc = "Loot your own castle, gaining +" + m_lootVal + " gold but dealing " + m_damageVal + " damage to the castle.";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        m_playerUnlockLevel = 2;

        SetupBasicData();
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GameHelper.GetPlayer().Castle.GetHit(m_damageVal);
        GameHelper.GetPlayer().m_wallet.AddResources(new GameWallet(m_lootVal));
    }
}
