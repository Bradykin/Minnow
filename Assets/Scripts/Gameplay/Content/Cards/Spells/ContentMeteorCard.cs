using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMeteorCard : GameCardSpellBase
{
    public ContentMeteorCard()
    {
        m_spellEffect = 50;

        m_name = "Meteor";
        m_targetType = Target.Enemy;
        m_cost = 3;
        m_rarity = GameRarity.Rare;

        SetupBasicData();

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.DamageSpell);

        m_onPlaySFX = AudioHelper.LargeImpact;
    }

    public override string GetDesc()
    {
        string startingDesc = GetDamageDescString();

        return startingDesc;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.GetHitBySpell(GetSpellValue(), this);
    }
}