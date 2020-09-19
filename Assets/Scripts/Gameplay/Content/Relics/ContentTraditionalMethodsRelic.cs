using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTraditionalMethodsRelic : GameRelic
{
    public ContentTraditionalMethodsRelic()
    {
        m_name = "Traditional Methods";
        m_desc = "Firebolts deal +2 damage, Cure Wounds heal for 3 additional healing, and Dwarven Soldiers get +1/+1 on Summon.";
        m_rarity = GameRarity.Event;

        LateInit();
    }
}
