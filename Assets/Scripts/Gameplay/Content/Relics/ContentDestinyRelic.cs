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

        m_tags.AddTag(GameTag.TagType.Reanimate);
        m_tags.AddTag(GameTag.TagType.Scaler);
        m_tags.AddTag(GameTag.TagType.Tank);
    }
}
