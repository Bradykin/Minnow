using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentToxicTonicCard : GameCardSpellBase
{
    private int m_healthBarrier = 80;

    public ContentToxicTonicCard()
    {
        m_spellEffect = 2;

        m_name = "Toxic Tonic";
        m_targetType = Target.Enemy;
        m_cost = 4;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.DamageSpell);

        m_onPlaySFX = AudioHelper.MagicEffect;
    }

    public override string GetDesc()
    {
        return "Instantly kill an enemy unit with " + m_healthBarrier + " or less health.";
    }

    public override bool IsValidToPlay(GameUnit targetUnit)
    {
        return base.IsValidToPlay(targetUnit) && targetUnit.GetCurHealth() <= m_healthBarrier;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.Die();
    }
}