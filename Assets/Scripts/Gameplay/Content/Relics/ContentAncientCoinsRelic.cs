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

        m_tags.AddTag(GameTag.TagType.Gold);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
