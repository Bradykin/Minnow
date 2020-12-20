﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarkedForDeathCard : GameCardSpellBase
{   
    public ContentMarkedForDeathCard()
    {
        m_spellEffect = 2;

        m_name = "Marked for Death";
        m_targetType = Target.Enemy;
        m_cost = 2;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        m_keywordHolder.AddKeyword(new GameEnrageKeyword(null));
        m_keywordHolder.AddKeyword(new GameBrittleKeyword());

        SetupBasicData();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilitySpell);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Brittle);

        m_onPlaySFX = AudioHelper.SmallDebuff;
    }

    public override string GetDesc()
    {
        return "Target enemy unit gains '<b>Enrage</b>: Gain <b>Brittle</b>.\n";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameEnrageKeyword(new GameGainBrittleAction(targetUnit)), false, false);
    }
}
