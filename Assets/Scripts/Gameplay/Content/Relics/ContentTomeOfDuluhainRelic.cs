using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTomeOfDuluhainRelic : GameRelic
{
    public ContentTomeOfDuluhainRelic()
    {
        m_name = "Tome of Duluhain";
        m_desc = "-1 Spell Power, all spells cost 1 less.";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }
}
