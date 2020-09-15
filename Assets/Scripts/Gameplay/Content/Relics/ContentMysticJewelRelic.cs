using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMysticJewelRelic : GameRelic
{
    public ContentMysticJewelRelic()
    {
        m_name = "Mystic Jewel";
        m_desc = "On the last two turns of a wave, gain additional energy and draw additional cards equal to the number of mystics in play.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
