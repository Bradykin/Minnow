using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEyeOfTelloRelic : GameRelic
{
    public ContentEyeOfTelloRelic()
    {
        m_name = "Eye of Tello";
        m_desc = "+4 actions during intermission phase!";
        m_rarity = GameRarity.Special;

        LateInit();
    }
}
