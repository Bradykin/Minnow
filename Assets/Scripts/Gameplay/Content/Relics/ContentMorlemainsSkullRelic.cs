using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMorlemainsSkullRelic : GameRelic
{
    public ContentMorlemainsSkullRelic()
    {
        m_name = "Morlemains Skull";
        m_desc = "Whenever an enemy is killed, gain 1 energy.";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }
}
