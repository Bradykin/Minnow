﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStaminaTrainingCard : GameCardSpellBase
{
    public ContentStaminaTrainingCard()
    {
        m_name = "Stamina Training";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Starter;
        m_shouldExile = true;

        InitializeWithLevel(GetCardLevel());

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        if (GetCardLevel() >= 2)
        {
            return "Give an allied unit +" + m_spellEffect + spString + " max Stamina.\nFill the target's Stamina.\n" + GetModifiedBySpellPowerString() + "\n\n<i>(Buffs are permanent)</i>";
        }
        else
        {
            return "Give an allied unit +" + m_spellEffect + spString + " max Stamina.\n" + GetModifiedBySpellPowerString() + "\n\n<i>(Buffs are permanent)</i>";
        }
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddMaxStamina(GetSpellValue());

        if (GetCardLevel() >= 2)
        {
            targetUnit.FillStamina();
        }
    }

    public override void InitializeWithLevel(int level)
    {
        m_cost = 1;
        m_spellEffect = 2;

        if (level >= 1)
        {
            m_spellEffect = 3;
        }

        if (level >= 2)
        {
            //Also fill curStamina
        }
    }
}
