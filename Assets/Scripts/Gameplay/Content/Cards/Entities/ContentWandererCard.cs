using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWandererCard : GameCardEntityBase
{
    public ContentWandererCard()
    {
        m_entity = new ContentWanderer();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Shiv);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
