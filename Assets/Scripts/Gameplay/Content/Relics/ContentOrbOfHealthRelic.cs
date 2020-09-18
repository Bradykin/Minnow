using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrbOfHealthRelic : GameRelic
{
    public ContentOrbOfHealthRelic()
    {
        m_name = "Orb of Health";
        m_desc = "Increase the max health of all entities you control by 6!";
        m_rarity = GameRarity.Common;

        LateInit();
    }
}
