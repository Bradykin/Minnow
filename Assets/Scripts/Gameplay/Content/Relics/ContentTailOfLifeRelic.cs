using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTailOfLifeRelic : GameRelic
{
    private bool m_hasUsed;

    public ContentTailOfLifeRelic()
    {
        m_name = "Tail of Life";
        m_desc = "When your castle would next be destroyed, instead set it to 50 health.  This will only happen once.";
        m_rarity = GameRarity.Uncommon;

        LateInit();
    }

    public void Use()
    {
        m_hasUsed = true;
        m_desc = "The power of the tail has been consumed and will work no more.";
    }

    public bool HasUsed()
    {
        return m_hasUsed;
    }
}
