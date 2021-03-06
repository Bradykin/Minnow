﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBurningShivsRelic : GameRelic
{
    public ContentBurningShivsRelic()
    {
        m_name = "Burning Shivs";
        m_desc = "Shivs hit three times.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Shiv, 3);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
    }
}
