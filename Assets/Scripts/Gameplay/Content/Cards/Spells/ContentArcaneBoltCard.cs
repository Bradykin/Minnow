﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentArcaneBoltCard : GameCardSpellBase
{
    public ContentArcaneBoltCard()
    {
        m_spellEffect = 3;

        m_name = "Arcane Bolt";
        m_desc = "Deal " + GetSpellValue() + " damage to a target.  Double benefits from spell power.";
        m_playDesc = "BOOM!";
        m_targetType = Target.Enemy;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.GetHit(GetSpellValue());
    }

    protected override int GetSpellValue()
    {
        return 2 * base.GetSpellValue();
    }
}