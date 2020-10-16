using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLoadedChestRelic : GameRelic
{
    public ContentLoadedChestRelic()
    {
        m_name = "Loaded Chest";
        m_desc = "On pickup, gain 200 gold.";
        m_rarity = GameRarity.Starter;

        LateInit();
    }
}
