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

        m_cost = 1;
        m_spellEffect = 1;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"<b>Permanently</b> give an allied unit +{UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} max Stamina.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddMaxStamina(GetSpellValue(), true);
    }
}
