using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAngelicFeatherRelic : GameRelic
{
    public ContentAngelicFeatherRelic()
    {
        m_name = "Angelic Feather";
        m_desc = "When an allied unit survives a hit with 10 or less health; it gains <b>Damage Shield</b>.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Tank);
        m_tags.AddTag(GameTag.TagType.Healing);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}