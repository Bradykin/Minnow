using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMysticForesightRelic : GameRelic
{
    public ContentMysticForesightRelic()
    {
        m_name = "Mystic Foresight";
        m_desc = "On the last two turns of a wave, gain additional energy and draw additional cards equal to the number of mystics in play.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
