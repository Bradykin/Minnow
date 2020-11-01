using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAncientMysteryRelic : GameRelic
{
    public ContentAncientMysteryRelic()
    {
        m_name = "Ancient Mystery";
        m_desc = "<b>Knowledgable</b> triggers twice.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Spellcraft);
        m_tags.AddTag(GameTag.TagType.Humanoid);
    }
}
