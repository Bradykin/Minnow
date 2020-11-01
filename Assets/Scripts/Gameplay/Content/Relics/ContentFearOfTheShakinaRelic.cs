using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFearOfTheShakinaRelic : GameRelic
{
    public ContentFearOfTheShakinaRelic()
    {
        m_name = "Fear of the Shakina";
        m_desc = "When an enemy unit steps out of fog, it takes 10 damage.";
        m_rarity = GameRarity.Common;

        LateInit();
    }
}
