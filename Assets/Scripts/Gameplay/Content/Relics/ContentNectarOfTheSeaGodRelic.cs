﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNectarOfTheSeaGodRelic : GameRelic
{
    public ContentNectarOfTheSeaGodRelic()
    {
        m_name = "Nectar of the Sea God";
        m_desc = "Allied units within range 1 of a water tile get +3/+3.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Water);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);
    }
}