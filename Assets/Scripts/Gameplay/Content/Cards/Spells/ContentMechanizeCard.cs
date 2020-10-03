﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMechanizeCard : GameCardSpellBase
{
    public ContentMechanizeCard()
    {
        m_name = "Mechanize";
        m_desc = "Target allied <b>Creation</b> unit loses all current Stamina and gains that much power.";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.MaxStamina);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }

    public override bool IsValidToPlay(GameUnit targetUnit)
    {
        return base.IsValidToPlay() && targetUnit.GetTypeline() == Typeline.Creation;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        int curStamina = targetUnit.GetCurStamina();
        targetUnit.SpendStamina(curStamina);
        targetUnit.AddPower(curStamina);
    }
}