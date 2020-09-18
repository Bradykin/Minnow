﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTotemOfTheWolfRelic : GameRelic
{
    public ContentTotemOfTheWolfRelic()
    {
        m_name = "Totem of the Wolf";
        m_desc = "One turn per wave, enter the full moon. Your units will gain double AP regen and attacking costs 1 less (minimum of 1).";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}