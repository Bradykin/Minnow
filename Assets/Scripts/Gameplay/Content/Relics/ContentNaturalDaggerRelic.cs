using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNaturalDaggerRelic : GameRelic
{
    public ContentNaturalDaggerRelic()
    {
        m_name = "Natural Dagger";
        m_desc = "Friendly attacks ignore terrain damage resistance.";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }
}
