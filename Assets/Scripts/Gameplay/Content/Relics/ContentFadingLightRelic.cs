using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFadingLightRelic : GameRelic
{
    public ContentFadingLightRelic()
    {
        m_name = "Fading Light";
        m_desc = "Allied units fully heal at the start of each turn, but get -2 sight range.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Healing);
        m_tags.AddTag(GameTag.TagType.Tank);
    }
}
