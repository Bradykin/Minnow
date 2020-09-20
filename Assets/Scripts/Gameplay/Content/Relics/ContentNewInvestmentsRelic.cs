﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNewInvestmentsRelic : GameRelic
{
    public ContentNewInvestmentsRelic()
    {
        m_name = "New Investments";
        m_desc = "At the end of each wave, gain gold equal to 15 * the current wave number.";
        m_rarity = GameRarity.Event;

        LateInit();
    }
}