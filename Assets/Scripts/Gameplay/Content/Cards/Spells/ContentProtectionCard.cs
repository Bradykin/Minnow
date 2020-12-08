﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentProtectionCard : GameCardSpellBase
{
    public ContentProtectionCard()
    {
        m_name = "Protection";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Common;
        m_xSpell = true;

        m_cost = 0;

        m_keywordHolder.AddKeyword(new GameDamageShieldKeyword(-1));

        m_tags.AddTag(GameTag.TagType.Tank);
        m_tags.AddTag(GameTag.TagType.DamageShield);

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        return "Target allied unit gains <b>Damage Shield</b> X.\n";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        targetUnit.AddKeyword(new GameDamageShieldKeyword(GameHelper.GetPlayer().GetXValue()), false, false);

        base.PlayCard(targetUnit);
    }
}
