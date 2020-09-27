using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRangerCard : GameCardEntityBase
{
    public ContentRangerCard()
    {
        m_entity = new ContentRanger();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Forest);
    }
}
