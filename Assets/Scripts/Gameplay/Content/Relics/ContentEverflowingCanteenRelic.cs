using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEverflowingCanteenRelic : GameRelic
{
    public ContentEverflowingCanteenRelic()
    {
        m_name = "Everflowing Canteen";
        m_desc = "Allied units get <b>Damage Reduction 2</b> when within range 1 of a water tile.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Water);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Tank);
    }
}