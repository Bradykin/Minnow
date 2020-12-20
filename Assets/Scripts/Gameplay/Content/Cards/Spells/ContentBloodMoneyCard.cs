﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBloodMoneyCard : GameCardSpellBase
{
    public ContentBloodMoneyCard()
    {
        m_name = "Blood Money";
        m_desc = "Target allied unit gains '<b>Enrage</b>: Gain gold equal to the damage taken.'";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        m_keywordHolder.AddKeyword(new GameEnrageKeyword(null));

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Enrage);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Healing, isReceiver: false);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Gold, 3);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilitySpell); 

        m_onPlaySFX = AudioHelper.GoldSpell;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameEnrageKeyword(new GameGainGoldEnrageAction(targetUnit, 1)), false, false);
    }
}