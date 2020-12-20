using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAncientCoinsRelic : GameRelic
{
    public ContentAncientCoinsRelic()
    {
        m_name = "Ancient Coins";
        m_desc = "Whenever an elite dies, gain 50 gold.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Gold);
        m_tagHolder.AddTag(GameTagHolder.TagType.Midrange);
    }
}
