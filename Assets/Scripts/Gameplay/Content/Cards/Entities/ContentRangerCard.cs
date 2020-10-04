using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRangerCard : GameUnitCard
{
    public ContentRangerCard()
    {
        m_unit = new ContentRanger();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Forest);
    }
}
