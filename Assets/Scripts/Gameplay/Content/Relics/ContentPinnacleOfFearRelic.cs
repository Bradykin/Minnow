using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPinnacleOfFearRelic : GameRelic
{
    public ContentPinnacleOfFearRelic()
    {
        m_name = "Pinnacle of Fear";
        m_desc = "Entities cost 1 less, but draw 1 less each turn.";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }
}
