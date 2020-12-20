using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPinnacleOfFearRelic : GameRelic
{
    public ContentPinnacleOfFearRelic()
    {
        m_name = "Pinnacle of Fear";
        m_desc = "Units cost 1 less energy, but you draw 1 less card each turn.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.HighCost);
        m_tagHolder.AddTag(GameTagHolder.TagType.Knowledgeable);
        m_tagHolder.AddTag(GameTagHolder.TagType.Scaler);
    }
}
