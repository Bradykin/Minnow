﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentImpaliumRelic : GameRelic
{
    public ContentImpaliumRelic()
    {
        m_name = "Impalium";
        m_desc = "<b>Spellcraft</b> triggers twice.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Spellcraft);
        m_tags.AddTag(GameTag.TagType.Shiv);
        m_tags.AddTag(GameTag.TagType.MagicPower);
    }
}
