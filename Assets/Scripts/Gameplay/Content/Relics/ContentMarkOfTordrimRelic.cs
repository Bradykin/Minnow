using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarkOfTordrimRelic : GameRelic
{
    public ContentMarkOfTordrimRelic()
    {
        m_name = "Mark of Tordrim";
        m_desc = "When you summon a unit, give it a random positive ability.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
