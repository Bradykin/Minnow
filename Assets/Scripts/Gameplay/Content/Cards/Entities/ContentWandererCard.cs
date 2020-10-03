using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWandererCard : GameUnitCardBase
{
    public ContentWandererCard()
    {
        m_unit = new ContentWanderer();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Shiv);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
