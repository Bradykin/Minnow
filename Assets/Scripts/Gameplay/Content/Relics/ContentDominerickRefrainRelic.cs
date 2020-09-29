using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDominerickRefrainRelic : GameRelic
{
    public ContentDominerickRefrainRelic()
    {
        m_name = "Dominerick Refrain";
        m_desc = "+2 Spell Power";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }
}
