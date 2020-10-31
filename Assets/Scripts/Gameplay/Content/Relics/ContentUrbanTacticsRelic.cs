﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentUrbanTacticsRelic : GameRelic
{
    public ContentUrbanTacticsRelic()
    {
        m_name = "Urban Tactics";
        m_desc = "All allied units need 1 less Stamina to attack (minimum of 1), but have -2 Stamina regen.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.UtilitySpell);
        m_tags.AddTag(GameTag.TagType.MaxStamina);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }
}
