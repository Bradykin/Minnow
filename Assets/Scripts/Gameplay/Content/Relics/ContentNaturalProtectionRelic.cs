using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNaturalProtectionRelic : GameRelic
{
    public ContentNaturalProtectionRelic()
    {
        m_name = "Natural Protection";
        m_desc = "Ally units get doubled the positive benefits from all terrain tiles.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
