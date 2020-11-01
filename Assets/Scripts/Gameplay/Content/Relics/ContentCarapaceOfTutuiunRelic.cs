﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCarapaceOfTutuiun : GameRelic
{
    public ContentCarapaceOfTutuiun()
    {
        m_name = "Carapace of Tutuiun";
        m_desc = "Allied units have -1 stamina regen, but when they are summoned, they get Damage Reduction 2.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Tank);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
    }
}