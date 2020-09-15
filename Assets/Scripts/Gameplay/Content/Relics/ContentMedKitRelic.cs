using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMedKitRelic : GameRelic
{
    public ContentMedKitRelic()
    {
        m_name = "Med Kit";
        m_desc = "Humanoid creatures heal equal to the terrain cost of the tile they are on at the end of each round.";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }
}
