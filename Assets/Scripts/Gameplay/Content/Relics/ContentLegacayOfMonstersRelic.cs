using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLegacayOfMonstersRelic : GameRelic
{
    public ContentLegacayOfMonstersRelic()
    {
        m_name = "Legacy of Monsters";
        m_desc = "When an ally monster is summoned, it gains +1 power";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }
}
