using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNaturalProtectionRelic : GameRelic
{
    public ContentNaturalProtectionRelic()
    {
        m_name = "Natural Protection";
        m_desc = "Allied units get doubled the positive benefits from all terrain tiles.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Forest);
        m_tags.AddTag(GameTag.TagType.Tank);
    }
}
