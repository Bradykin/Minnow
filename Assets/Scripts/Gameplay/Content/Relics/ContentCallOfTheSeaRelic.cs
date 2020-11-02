using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCallOfTheSeaRelic : GameRelic
{
    public ContentCallOfTheSeaRelic()
    {
        m_name = "Call of the Sea";
        m_desc = "Whenever an allied unit starts its turn next to a water tile; it heals for 10.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Healing);
        m_tags.AddTag(GameTag.TagType.Water);
    }
}
