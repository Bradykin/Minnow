﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSachelOfDeceptionRelic : GameRelic
{
    public ContentSachelOfDeceptionRelic()
    {
        m_name = "Sachel of Deception";
        m_desc = "Whenever an allied unit without a keyword would die, instead give it '<b>Death</b>: +3/+3' and return it to full health.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Reanimate);
        m_tags.AddTag(GameTag.TagType.Creation);
    }
}
