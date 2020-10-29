﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBloodMoneyCard : GameCardSpellBase
{
    public ContentBloodMoneyCard()
    {
        m_name = "Blood Money";
        m_desc = "Target allied unit gains <b>Enrage</b>: Gain gold equal to the damage taken.";
        m_targetType = Target.Ally;
        m_cost = 5;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        m_playerUnlockLevel = 2;

        m_keywordHolder.AddKeyword(new GameEnrageKeyword(null));

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Gold);
        m_tags.AddTag(GameTag.TagType.UtilitySpell);
        m_tags.AddTag(GameTag.TagType.Enrage);
        m_tags.AddTag(GameTag.TagType.Healing);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameEnrageKeyword(new GameGainGoldEnrageAction(targetUnit)));
    }
}