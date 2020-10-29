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

        m_audioCategory = AudioHelper.SpellAudioCategory.Damage;
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        if (GameHelper.GetPlayer().GetCastleGameElement() == null)
        {
            return;
        }

        base.PlayCard();

        if (GameHelper.GetPlayer().GetCastleGameElement() is ContentCastleBuilding castleBuilding)
        {
            castleBuilding.GetHit(m_damageVal);
        }
        else if (GameHelper.GetPlayer().GetCastleGameElement() is ContentRoyalCaravan castleUnit)
        {
            castleUnit.GetHit(m_damageVal);
        }

        GameHelper.GetPlayer().m_wallet.AddResources(new GameWallet(m_lootVal));
    }
}
