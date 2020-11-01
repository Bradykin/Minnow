﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentIotalRelic : GameRelic
{
    public ContentIotalRelic()
    {
        m_name = "Iotal";
        m_desc = "Allied units get +1 stamina regen per 250 gold you have (rounded down).";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        //TODO: Alex - When the push/pull tags can be manually set, this needs a pull gold tag.
    }
}