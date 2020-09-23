using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDestinyRelic : GameRelic
{
    public ContentDestinyRelic()
    {
        m_name = "Destiny";
        m_desc = "When a friendly unit would die, 33% chance it instead survives at 1 health.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
