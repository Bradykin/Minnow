using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDestinyRelic : GameRelic
{
    public ContentDestinyRelic()
    {
        m_name = "Destiny";
        m_desc = "When an allied unit would die, 33% chance it instead survives at 1 health.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Reanimate);
        m_tagHolder.AddTag(GameTagHolder.TagType.Scaler);
        m_tagHolder.AddTag(GameTagHolder.TagType.Tank);
    }
}
