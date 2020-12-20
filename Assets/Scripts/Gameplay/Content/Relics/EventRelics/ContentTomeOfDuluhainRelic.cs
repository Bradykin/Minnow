﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTomeOfDuluhainRelic : GameRelic
{
    public ContentTomeOfDuluhainRelic()
    {
        m_name = "Tome of Duluhain";
        m_desc = "-3 <b>Magic Power</b>, all spells cost 1 less energy.";
        m_rarity = GameRarity.Special;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.LowCost);
        m_tagHolder.AddTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddTag(GameTagHolder.TagType.UtilitySpell);
    }
}
