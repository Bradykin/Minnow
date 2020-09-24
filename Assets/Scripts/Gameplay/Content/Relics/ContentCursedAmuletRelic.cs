﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCursedAmuletRelic : GameRelic
{
    public ContentCursedAmuletRelic()
    {
        m_name = "Cursed Amulet";
        m_desc = "When an ally dies, all adjacent enemies are drained of all action points.";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }
}
