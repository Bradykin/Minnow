﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPlagueMaskRelic : GameRelic
{
    public ContentPlagueMaskRelic()
    {
        m_name = "Plague Mask";
        m_desc = "Allied Monsters have <b>Regenerate 5</b>.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.Healing);
    }
}