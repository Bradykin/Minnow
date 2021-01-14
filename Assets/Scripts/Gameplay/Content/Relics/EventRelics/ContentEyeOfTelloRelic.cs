using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEyeOfTelloRelic : GameRelic
{
    public ContentEyeOfTelloRelic()
    {
        m_name = "Eye of Tello";
        m_desc = "Does nothing"; //nmartino - Rework this
        m_rarity = GameRarity.Special;

        LateInit();
    }
}
