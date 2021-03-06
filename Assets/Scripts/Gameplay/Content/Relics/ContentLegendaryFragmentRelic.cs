﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLegendaryFragmentRelic : GameRelic
{
    public ContentLegendaryFragmentRelic()
    {
        m_name = "Legendary Fragment";
        m_desc = "All friendly units get -2/-0, but gain +1 Stamina regen.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.StaminaRegen);
    }
}
