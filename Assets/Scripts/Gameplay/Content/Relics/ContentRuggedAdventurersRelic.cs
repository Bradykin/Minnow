using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRuggedAdventurersRelic : GameRelic
{
    public ContentRuggedAdventurersRelic()
    {
        m_name = "Rugged Adventurers";
        m_desc = "Humanoid creatures heal equal to the terrain cost of the tile they are on at the end of each round.";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }
}
