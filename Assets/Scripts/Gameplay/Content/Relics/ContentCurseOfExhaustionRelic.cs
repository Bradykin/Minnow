using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCurseOfExhaustionRelic : GameRelic
{
    public ContentCurseOfExhaustionRelic()
    {
        m_name = "Curse of Exhaustion";
        m_desc = "When an ally dies, the attacker is drained of all action points.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
