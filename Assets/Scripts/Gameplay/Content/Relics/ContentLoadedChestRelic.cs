using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLoadedChestRelic : GameRelic
{
    public ContentLoadedChestRelic()
    {
        SetRelicLevel(GetRelicLevel());

        m_name = "Loaded Chest";
        m_desc = "On pickup, gain " + (75 * (m_relicLevel + 1)) + " gold.";
        m_rarity = GameRarity.Starter;

        LateInit();
    }
}
