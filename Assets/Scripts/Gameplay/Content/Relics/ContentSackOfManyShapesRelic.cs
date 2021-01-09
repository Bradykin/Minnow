using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSackOfManyShapesRelic : GameRelic
{
    public ContentSackOfManyShapesRelic()
    {
        m_name = "Sack of Many Shapes";
        m_desc = "Draw 3 extra cards, and gain 2 extra energy on the first turn of each wave.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.EnergyRegen);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.HighCost, isReceiver: false);
    }
}
