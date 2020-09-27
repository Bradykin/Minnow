using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNaturalScoutCard : GameCardEntityBase
{
    public ContentNaturalScoutCard()
    {
        m_entity = new ContentNaturalScout();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Explorer);
    }
}
