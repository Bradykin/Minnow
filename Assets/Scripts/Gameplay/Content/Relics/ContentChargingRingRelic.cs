using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentChargingRingRelic : GameRelic
{
    public ContentChargingRingRelic()
    {
        m_name = "Charging Ring";
        m_desc = "Allied <b>Monster</b> units get '<b>Momentum</b>: +1/+1'.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Monster);
        m_tagHolder.AddTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddTag(GameTagHolder.TagType.Momentum);
    }
}
