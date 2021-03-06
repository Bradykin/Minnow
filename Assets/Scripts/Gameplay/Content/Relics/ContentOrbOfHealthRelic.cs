﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrbOfHealthRelic : GameRelic
{
    public ContentOrbOfHealthRelic()
    {
        m_name = "Orb of Health";
        m_desc = "Give all allied units +0/+6.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);
    }
}
