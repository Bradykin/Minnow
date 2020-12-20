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

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Shiv);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.DamageSpell);
    }
}
