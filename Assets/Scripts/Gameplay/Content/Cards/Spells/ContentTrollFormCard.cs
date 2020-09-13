using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTrollFormCard : GameCardSpellBase
{
    public ContentTrollFormCard()
    {
        m_spellEffect = 3;

        m_name = "Troll Form";
        m_playDesc = "The target begins to regenerate!";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;
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

        return "Grant an ally regen " + m_spellEffect + spString + ".\n" + GetModifiedBySpellPowerString();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddKeyword(new GameRegenerateKeyword(GetSpellValue()));
    }
}