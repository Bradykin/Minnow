using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEyeOfTelsimirRelic : GameRelic
{
    public ContentEyeOfTelsimirRelic()
    {
        m_name = "Eye of Telsimir";
        m_desc = "When you gain this, all water on the map dries up.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
