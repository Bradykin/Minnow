using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSoulTrapRelic : GameRelic
{
    public ContentSoulTrapRelic()
    {
        m_name = "Soul Trap";
        m_desc = "When an ally dies, draw a card.";
        m_rarity = GameRarity.Common;

        LateInit();
    }
}
