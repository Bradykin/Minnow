using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEncouragementCard : GameCardSpellBase
{
    public ContentEncouragementCard()
    {
        m_name = "Encouragement";
        m_desc = "Deal 1 damage to target allied unit, then give them +0/+3.";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Enrage);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
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

        targetUnit.AddStats(0, 3, false, false);
    }
}