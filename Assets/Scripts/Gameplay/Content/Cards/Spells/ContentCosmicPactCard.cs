﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCosmicPactCard : GameCardSpellBase
{
    public ContentCosmicPactCard()
    {
        m_name = "Cosmic Pact";
        m_desc = "For the rest of this wave, all spells in your current hand cost 1 less.";
        m_targetType = Target.None;
        m_cost = 3;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        m_playerUnlockLevel = 1;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.UtilitySpell);
        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.MagicPower);
        m_tags.AddTag(GameTag.TagType.Spellcraft);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        GamePlayer player = GameHelper.GetPlayer();
        if (player == null)
        {
            return;
        }

        for (int i = 0; i < player.m_hand.Count; i++)
        {
            if (!(player.m_hand[i] is GameCardSpellBase))
            {
                continue;
            }
            player.m_hand[i].SetTempCost(-1);
        }
    }
}