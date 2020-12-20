using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLegacyOfMonstersRelic : GameRelic
{
    public ContentLegacyOfMonstersRelic()
    {
        m_name = "Legacy of Monsters";
        m_desc = "Allied <b>Monster</b> units get +1 Stamina regen.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.StaminaRegen);
        m_tagHolder.AddTag(GameTagHolder.TagType.Monster);
    }
}
