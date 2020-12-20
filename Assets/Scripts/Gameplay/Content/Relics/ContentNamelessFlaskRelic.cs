using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNamelessFlaskRelic : GameRelic
{
    public ContentNamelessFlaskRelic()
    {
        m_name = "Nameless Flask";
        m_desc = "Allied units can attack when they have exactly 1 Stamina.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.StaminaRegen);
    }
}
