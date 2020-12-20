using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTotemOfRevengeRelic : GameRelic
{
    public ContentTotemOfRevengeRelic()
    {
        m_name = "Totem of Revenge";
        m_desc = "When an allied unit dies, all allied units in range 3 fill their stamina.";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.StaminaRegen);
        m_tagHolder.AddTag(GameTagHolder.TagType.MaxStamina);
        m_tagHolder.AddTag(GameTagHolder.TagType.Creation);
        m_tagHolder.AddTag(GameTagHolder.TagType.Reanimate);
    }
}
