using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEncouragementCard : GameCardSpellBase
{
    public ContentEncouragementCard()
    {
        m_name = "Encouragement";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;
        m_spellEffect = 2;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Enrage);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }
        return $"Deal 1 damage to target allied unit, then give them +{UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}/+0.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.GetHitBySpell(1, this);

        if (targetUnit.m_isDead)
        {
            return;
        }

        targetUnit.AddStats(GetSpellValue(), 0, false, false);
    }
}