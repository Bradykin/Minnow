﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRunicBladeCard : GameCardSpellBase
{
    public ContentRunicBladeCard()
    {
        m_name = "Runic Blade";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        m_keywordHolder.AddKeyword(new GameVictoriousKeyword(null));
        m_keywordHolder.AddKeyword(new GameSpellcraftKeyword(null));

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Victorious);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft, 3);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.DaggerSwingSpell;
    }

    public override string GetDesc()
    {
        return "Target allied unit gains '<b>Victorious</b>: Trigger <b>Spellcraft</b>'.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameVictoriousKeyword(new GameSpellcraftAttackAction(targetUnit, 1)), false, false);
    }
}