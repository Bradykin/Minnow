﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBullheadedCard : GameCardSpellBase
{
    public ContentBullheadedCard()
    {
        m_name = "Bullheaded";
        m_desc = "Target allied unit gains '<b>Enrage</b>: +1/+0.'";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        m_keywordHolder.AddKeyword(new GameEnrageKeyword(null));

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

        targetUnit.AddKeyword(new GameEnrageKeyword(new GameGainStatsAction(targetUnit, 1, 0)), false, false);
    }
}