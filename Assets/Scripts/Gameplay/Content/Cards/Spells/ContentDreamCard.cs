﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDreamCard : GameCardSpellBase
{
    public ContentDreamCard()
    {
        m_spellEffect = 2;

        m_name = "Dream";
        m_playDesc = "You draw some cards!";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        return "Draw " + m_spellEffect + spString + " cards.\n" + GetModifiedBySpellPowerString();
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        player.DrawCards(GetSpellValue());
    }
}