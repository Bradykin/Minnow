using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLoadedChestRelic : GameRelic
{
    public ContentLoadedChestRelic()
    {
        m_name = "Loaded Chest";
        m_desc = "On pickup, gain " + (75 * (GetRelicLevel() + 1)) + " gold.";
        m_rarity = GameRarity.Starter;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Gold);
    }
}
