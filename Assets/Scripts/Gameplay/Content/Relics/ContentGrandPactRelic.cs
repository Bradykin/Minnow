using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrandPactRelic : GameRelic
{
    public ContentGrandPactRelic()
    {
        m_name = "Grand Pact";
        m_desc = "If you have at least one unit of each type in play, all ally units get +1 ap regen.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
