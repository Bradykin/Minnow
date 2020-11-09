﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrbOfEnergyRelic : GameRelic
{
    public ContentOrbOfEnergyRelic()
    {
        m_name = "Orb of Energy";
        m_desc = "Increase max energy by " + (GetRelicLevel() + 1) + ".";
        m_rarity = GameRarity.Starter;

        LateInit();

        m_tags.AddTag(GameTag.TagType.HighCost);
    }
}