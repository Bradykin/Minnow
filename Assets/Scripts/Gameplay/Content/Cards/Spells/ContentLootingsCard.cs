﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLootingsCard : GameCardSpellBase
{
    private int m_lootVal = 30;
    private int m_damageVal = 10;

    public ContentLootingsCard()
    {
        m_name = "Lootings";
        m_desc = "Loot your own castle, gaining +" + m_lootVal + " gold but dealing " + m_damageVal + " damage to the castle.";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.GoldSpell;
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
            castleUnit.GetHitBySpell(m_damageVal, this);
        }

        GameHelper.GetPlayer().GainGold(m_lootVal, true);
    }
}
