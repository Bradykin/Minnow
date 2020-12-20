using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrandPactRelic : GameRelic
{
    public ContentGrandPactRelic()
    {
        m_name = "Grand Pact";
        m_desc = "If you have at least 1 Creation unit, 1 Monster unit, and 1 Humanoid unit in play, all allied units get +1 Stamina regen.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Creation);
        m_tagHolder.AddTag(GameTagHolder.TagType.Monster);
        m_tagHolder.AddTag(GameTagHolder.TagType.Humanoid);
        m_tagHolder.AddTag(GameTagHolder.TagType.StaminaRegen);
        m_tagHolder.AddTag(GameTagHolder.TagType.MaxStamina);
    }
}
