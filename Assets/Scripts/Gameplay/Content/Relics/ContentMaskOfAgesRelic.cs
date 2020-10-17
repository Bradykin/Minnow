using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMaskOfAgesRelic : GameRelic
{
    public ContentMaskOfAgesRelic()
    {
        SetRelicLevel(GetRelicLevel());

        m_name = "Mask of Ages";
        if (m_relicLevel == 0)
        {
            m_desc = "At the start of each turn, draw " + (m_relicLevel + 1) + " extra card.";
        }
        else
        {
            m_desc = "At the start of each turn, draw " + (m_relicLevel + 1) + " extra cards.";
        }
        m_rarity = GameRarity.Starter;

        LateInit();
    }
}
