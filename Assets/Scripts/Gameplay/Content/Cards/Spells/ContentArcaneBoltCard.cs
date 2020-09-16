using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentArcaneBoltCard : GameCardSpellBase
{
    public ContentArcaneBoltCard()
    {
        m_spellEffect = 4;

        m_name = "Arcane Bolt";
        m_playDesc = "BOOM!";
        m_targetType = Target.Enemy;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return GetDamageDescString() + "Triple benefits from spell power.";
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
        return 3 * (base.GetSpellValue() - m_spellEffect) + m_spellEffect;
    }
}
