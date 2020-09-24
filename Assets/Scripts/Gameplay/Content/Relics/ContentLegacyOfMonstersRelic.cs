using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLegacyOfMonstersRelic : GameRelic
{
    public ContentLegacyOfMonstersRelic()
    {
        m_name = "Legacy of Monsters";
        m_desc = "Allied monsters get +1 AP regen.";
        m_rarity = GameRarity.Common;

        LateInit();
    }
}
