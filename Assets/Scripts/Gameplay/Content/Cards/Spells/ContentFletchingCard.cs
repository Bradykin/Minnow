using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFletchingCard : GameCardSpellBase
{
    private int m_fletchlingCount = 8;

    public ContentFletchingCard()
    {
        m_name = "Fletching";
        m_desc = "Ally ranged units get +" + m_fletchlingCount + " power until end of turn.";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Range);
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        Globals.m_fletchingCount += m_fletchlingCount;
    }
}
