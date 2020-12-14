using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMysticWitchCard : GameUnitCard
{
    public ContentMysticWitchCard()
    {
        m_unit = new ContentMysticWitch();

        m_cost = 3;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.HighCost);
        m_tags.AddTag(GameTag.TagType.LowCost);
        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.Spellcraft);
    }
}
