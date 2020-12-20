using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBeaconOfSanityRelic : GameRelic
{
    public ContentBeaconOfSanityRelic()
    {
        m_name = "Beacon of Sanity";
        m_desc = "Allied units get +3 max Stamina, but get -1 sight range.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.MaxStamina);
    }
}
