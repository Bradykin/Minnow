using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNaturalScoutCard : GameUnitCard
{
    public ContentNaturalScoutCard()
    {
        m_unit = new ContentNaturalScout();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Explorer);
    }
}
