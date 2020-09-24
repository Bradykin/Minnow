using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPoisonedShivsRelic : GameRelic
{
    public ContentPoisonedShivsRelic()
    {
        m_name = "Poisoned Shivs";
        m_desc = "Shivs drain 2 action points from the target.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
