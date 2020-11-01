using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTheGreatestGiftRelic : GameRelic
{
    public ContentTheGreatestGiftRelic()
    {
        m_name = "The Greatest Gift";
        m_desc = "Allied units and buildings have +1 sight range.";
        m_rarity = GameRarity.Common;

        LateInit();
    }
}
