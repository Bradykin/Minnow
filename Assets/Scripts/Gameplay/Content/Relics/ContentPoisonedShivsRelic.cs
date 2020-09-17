using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPoisonedShivsRelic : GameRelic
{
    public ContentPoisonedShivsRelic()
    {
        m_name = "Poisoned Shivs";
        m_desc = "Shivs drain 1 action point from the target.";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }
}
