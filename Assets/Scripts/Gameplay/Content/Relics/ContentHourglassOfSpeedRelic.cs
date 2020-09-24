using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHourglassOfSpeedRelic : GameRelic
{
    public ContentHourglassOfSpeedRelic()
    {
        m_name = "Hourglass of Speed";
        m_desc = "Increase max AP of all friendly units by 1.";
        m_rarity = GameRarity.Common;

        LateInit();
    }
}
