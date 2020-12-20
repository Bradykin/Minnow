using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLoadedChestRelic : GameRelic
{
    public ContentLoadedChestRelic()
    {
        m_name = "Loaded Chest";
        m_desc = "Start with +75 gold.";
        m_rarity = GameRarity.Starter;

        LateInit();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.Gold);
    }
}
