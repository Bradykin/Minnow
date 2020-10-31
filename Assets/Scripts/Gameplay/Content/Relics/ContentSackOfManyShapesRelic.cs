using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSackOfManyShapesRelic : GameRelic
{
    public ContentSackOfManyShapesRelic()
    {
        m_name = "Sack of Many Shapes";
        m_desc = "Draw 3 extra cards, and gain 2 extra energy on the first turn of each wave.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.HighCost);
        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
