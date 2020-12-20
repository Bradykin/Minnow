using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCallOfTheSeaRelic : GameRelic
{
    public ContentCallOfTheSeaRelic()
    {
        m_name = "Call of the Sea";
        m_desc = "Allied units within range 1 of a water tile have <b>Regenerate 10</b>.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Healing);
        m_tagHolder.AddTag(GameTagHolder.TagType.Water);
    }
}
