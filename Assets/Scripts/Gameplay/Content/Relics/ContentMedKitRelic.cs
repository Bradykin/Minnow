using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMedKitRelic : GameRelic
{
    public ContentMedKitRelic()
    {
        m_name = "Med Kit";
        m_desc = "Allied Humanoid units heal equal to 3* the movement cost of the tile they are on at the start of each round.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Healing);
        m_tags.AddTag(GameTag.TagType.Humanoid);
        m_tags.AddTag(GameTag.TagType.Forest);
    }
}
