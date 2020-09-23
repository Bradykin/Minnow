using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLegendaryFragmentRelic : GameRelic
{
    public ContentLegendaryFragmentRelic()
    {
        m_name = "Legendary Fragment";
        m_desc = "-2 power, +1 AP regen to all friendly units";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
