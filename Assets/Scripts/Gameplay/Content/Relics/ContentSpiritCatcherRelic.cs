using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSpiritCatcherRelic : GameRelic
{
    public ContentSpiritCatcherRelic()
    {
        m_name = "Spirit Catcher";
        m_desc = "When an enemy unit dies, draw a card.";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }
}
