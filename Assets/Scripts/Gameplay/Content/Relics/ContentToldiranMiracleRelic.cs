using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentToldiranMiracleRelic : GameRelic
{
    public ContentToldiranMiracleRelic()
    {
        m_name = "Toldiran Miracle";
        m_desc = "When you summon a unit, if you control a <b>Humanoid</b> unit, a <b>Creation</b> unit, and a <b>Monster</b> unit; draw 2 cards and gain 3 energy. (The unit played does not count)";
        m_rarity = GameRarity.Uncommon;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.HighCost);
        m_tags.AddTag(GameTag.TagType.Humanoid);
        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.Creation);
    }
}