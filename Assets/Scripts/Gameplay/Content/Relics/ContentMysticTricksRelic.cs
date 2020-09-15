using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMysticTricksRelic : GameRelic
{
    public ContentMysticTricksRelic()
    {
        m_name = "Mystic Tricks";
        m_desc = "When an ";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
