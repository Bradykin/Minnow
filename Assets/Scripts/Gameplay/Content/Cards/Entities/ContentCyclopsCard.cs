using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCyclopsCard : GameCardEntityBase
{
    public ContentCyclopsCard()
    {
        m_entity = new ContentCyclops();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
