﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFletchingCard : GameCardSpellBase
{
    private int m_fletchlingCount = 8;

    public ContentFletchingCard()
    {
        m_name = "Fletching";
        m_desc = "Allied ranged units get +" + m_fletchlingCount + "/+0 until end of turn.";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Range);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GameHelper.GetPlayer().m_fletchingPowerIncrease += m_fletchlingCount;
    }
}
