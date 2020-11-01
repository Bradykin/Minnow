﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTauntingPipeRelic : GameRelic
{
    public ContentTauntingPipeRelic()
    {
        m_name = "Taunting Pipe";
        m_desc = "When summoned, allied <b>Humanoid</b> unit gain <b>Taunt</b>, allied <b>Monster</b> units get -1 stamina regen.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Tank);
        m_tags.AddTag(GameTag.TagType.Humanoid);
    }
}