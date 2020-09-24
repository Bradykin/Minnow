using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSecretSoupRelic : GameRelic
{
    public ContentSecretSoupRelic()
    {
        m_name = "Secret Soup";
        m_desc = "Increase the AP regen of <b>all units</b> by 1.";
        m_rarity = GameRarity.Rare;

        LateInit();
    }
}
