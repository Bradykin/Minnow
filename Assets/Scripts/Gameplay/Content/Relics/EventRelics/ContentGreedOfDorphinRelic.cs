using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGreedOfDorphinRelic : GameRelic
{
    public ContentGreedOfDorphinRelic()
    {
        m_name = "Greed of Dorphin";
        m_desc = "Gain 200 gold.";
        m_rarity = GameRarity.Special;

        LateInit();
    }
}