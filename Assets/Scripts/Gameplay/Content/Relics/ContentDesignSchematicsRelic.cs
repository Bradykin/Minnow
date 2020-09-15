using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesignSchematicsRelic : GameRelic
{
    public ContentDesignSchematicsRelic()
    {
        m_name = "Design Schematics";
        m_desc = "When a construct ally unit dies, return its card to the discard pile.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
