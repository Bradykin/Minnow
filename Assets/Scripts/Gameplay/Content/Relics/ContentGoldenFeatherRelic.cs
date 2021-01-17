using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoldenFeatherRelic : GameRelic
{
    public ContentGoldenFeatherRelic()
    {
        m_name = "Golden Feather";
        m_desc = "DOES NOTHING.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}