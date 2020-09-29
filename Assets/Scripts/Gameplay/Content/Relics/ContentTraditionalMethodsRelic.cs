using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTraditionalMethodsRelic : GameRelic
{
    public ContentTraditionalMethodsRelic()
    {
        m_name = "Traditional Methods";
        m_desc = "Aegis and Firebolt gain 'Draw a card', and Dwarven Soldiers gain +1 AP regen.";
        m_rarity = GameRarity.Event;

        LateInit();
    }
}
