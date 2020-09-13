using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentJoltCard : GameCardSpellBase
{
    public ContentJoltCard()
    {
        m_spellEffect = 1;

        m_name = "Jolt";
        m_desc = "Restore 1 AP.\nDraw a card.";
        m_playDesc = "The target gets a jolt of energy!";
        m_targetType = Target.Ally;
        m_cost = 0;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        return "Restore " + m_spellEffect + spString + " AP." + GetModifiedBySpellPowerString() + "\nDraw a card.";
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.GainAP(GetSpellValue());

        GameHelper.GetPlayer().DrawCard();
    }
}
