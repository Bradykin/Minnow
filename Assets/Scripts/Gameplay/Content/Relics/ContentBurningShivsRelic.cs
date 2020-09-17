using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBurningShivsRelic : GameRelic
{
    public ContentBurningShivsRelic()
    {
        m_name = "Burning Shivs";
        m_desc = "Shivs deal +3 damage.";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }
}
