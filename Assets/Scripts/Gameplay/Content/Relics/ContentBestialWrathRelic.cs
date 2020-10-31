using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBestialWrathRelic : GameRelic
{
    public ContentBestialWrathRelic()
    {
        m_name = "Bestial Wrath";
        m_desc = "Whenever a Momentum, Enrage, or Victorious trigger occurs on an allied Monster unit, trigger it an additional time.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Momentum);
        m_tags.AddTag(GameTag.TagType.Enrage);
        m_tags.AddTag(GameTag.TagType.Victorious);
        m_tags.AddTag(GameTag.TagType.Monster);
    }
}
