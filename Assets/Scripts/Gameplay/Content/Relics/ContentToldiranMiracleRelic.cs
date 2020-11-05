﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentToldiranMiracleRelic : GameRelic
{
    public ContentToldiranMiracleRelic()
    {
        m_name = "Toldiran Miracle";
        m_desc = "When you summon a unit, if you control a <b>Humanoid</b> unit, a <b>Creation</b> unit, and a <b>Monster</b> unit; draw a card and gain 2 energy.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.HighCost);
        m_tags.AddTag(GameTag.TagType.Humanoid);
        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.Creation);
    }
}