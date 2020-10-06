using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStaminaTrainingCard : GameCardSpellBase
{
    public ContentStaminaTrainingCard()
    {
        m_spellEffect = 1;

        m_name = "Stamina Training";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Starter;
        m_shouldExile = true;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        return "Give an allied unit +" + m_spellEffect + spString + " max Stamina.\n" + GetModifiedBySpellPowerString() + "\n\n<i>(Buffs are permanent)</i>";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddMaxStamina(GetSpellValue());
    }
}
