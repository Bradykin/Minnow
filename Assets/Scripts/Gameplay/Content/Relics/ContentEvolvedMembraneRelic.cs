using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEvolvedMembraneRelic : GameRelic
{
    public ContentEvolvedMembraneRelic()
    {
        m_name = "Evolved Membrane";
        m_desc = "When an allied unit is summoned, give it '<b>Victorious</b>: Get +1/+1'.";
        m_rarity = GameRarity.Common;

        LateInit();

        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.Victorious);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }
}
