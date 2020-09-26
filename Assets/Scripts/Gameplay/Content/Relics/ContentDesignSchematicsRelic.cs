using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesignSchematicsRelic : GameRelic
{
    public ContentDesignSchematicsRelic()
    {
        m_name = "Design Schematics";
        m_desc = "When a creation ally unit dies, return its card to the discard pile.";
        m_rarity = GameRarity.Common;

        LateInit();
    }
}
