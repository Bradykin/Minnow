using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTacticsOfMonRelic : GameRelic
{
    public ContentTacticsOfMonRelic()
    {
        m_name = "Tactics of Mon";
        m_desc = "Each turn, draw 2 extra cards and gain 2 extra energy.";
        m_rarity = GameRarity.Special;

        LateInit();
    }
}