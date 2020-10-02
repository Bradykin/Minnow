using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrowTalonsCard : GameCardSpellBase
{
    public ContentGrowTalonsCard()
    {
        m_spellEffect = 2;

        m_name = "Grow Talons";
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

        return "Give an allied unit +" + m_spellEffect + spString + "/+0.\n" + GetModifiedBySpellPowerString() + "\n\n<i>(Buffs are permanent)</i>";
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddPower(GetSpellValue());
    }
}
