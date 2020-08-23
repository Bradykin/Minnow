using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMaskOfAgesRelic : GameRelic
{
    public ContentMaskOfAgesRelic()
    {
        m_name = "Mask of Ages";
        m_desc = "At the start of each turn draw + 1";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
