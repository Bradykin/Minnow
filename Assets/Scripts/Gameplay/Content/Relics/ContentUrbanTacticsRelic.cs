using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentUrbanTacticsRelic : GameRelic
{
    public ContentUrbanTacticsRelic()
    {
        m_name = "Urban Tactics";
        m_desc = "All units need 1 less AP to attack (minimum of 1), but use up 1 more AP to move.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
