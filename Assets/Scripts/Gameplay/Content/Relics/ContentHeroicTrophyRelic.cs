﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHeroicTrophyRelic : GameRelic
{
    public ContentHeroicTrophyRelic()
    {
        m_name = "Heroic Trophy";
        m_desc = "Whenever an elite dies, give all living allied units +5/+5.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}