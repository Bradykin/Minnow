using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWildwoodSkirmisherCard : GameUnitCard
{
    public ContentWildwoodSkirmisherCard()
    {
        m_unit = new ContentWildwoodSkirmisher();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Forest);
    }
}
