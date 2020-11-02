using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEyeOfDorosonRelic : GameRelic
{
    public ContentEyeOfDorosonRelic()
    {
        m_name = "Eye of Doroson";
        m_desc = "When you gain this, all mountains on the map turn to dirt.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
