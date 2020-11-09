using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentToolOfTheDeadmanRelic : GameRelic
{
    public ContentToolOfTheDeadmanRelic()
    {
        m_name = "Tool of the Deadman";
        m_desc = "When an enemy unit dies add a <b>Shiv</b> to your hand.";
        m_rarity = GameRarity.Rare;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Shiv);
        m_tags.AddTag(GameTag.TagType.Spellcraft);
        m_tags.AddTag(GameTag.TagType.MagicPower);
        m_tags.AddTag(GameTag.TagType.DamageSpell);
    }
}
