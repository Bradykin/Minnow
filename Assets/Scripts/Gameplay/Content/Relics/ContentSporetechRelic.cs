﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSporetechRelic : GameRelic
{
    public ContentSporetechRelic()
    {
        m_name = "Sporetech";
        m_desc = "Randomize each unit's typeline when summoned (between Humanoid, Monster, and Creation).";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Humanoid);
        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.Monster);
    }
}
