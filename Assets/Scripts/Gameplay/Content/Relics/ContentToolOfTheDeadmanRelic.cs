using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentToolOfTheDeadmanRelic : GameRelic
{
    public ContentToolOfTheDeadmanRelic()
    {
        m_name = "Tool of the Deadman";
        m_desc = "When an enemy unit dies from an allied unit attacking it, add a <b>Shiv</b> to your hand.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tagHolder.AddTag(GameTagHolder.TagType.Shiv);
        m_tagHolder.AddTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddTag(GameTagHolder.TagType.DamageSpell);
        m_tagHolder.AddTag(GameTagHolder.TagType.StaminaRegen);
    }
}
