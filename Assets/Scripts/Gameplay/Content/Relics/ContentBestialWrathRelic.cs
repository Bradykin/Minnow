using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBestialWrathRelic : GameRelic
{
    public ContentBestialWrathRelic()
    {
        m_name = "Bestial Wrath";
        m_desc = "Whenever a Momentum, Enrage, or Victorious trigger occurs on an allied Monster unit, trigger it an additional time.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
