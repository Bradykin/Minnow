﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMaskOfAgesRelic : GameRelic
{
    public ContentMaskOfAgesRelic()
    {
        m_name = "Mask of Ages";
        m_desc = "At the start of each turn, draw an extra card.";
        m_rarity = GameRarity.Starter;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Knowledgeable);
    }
}
