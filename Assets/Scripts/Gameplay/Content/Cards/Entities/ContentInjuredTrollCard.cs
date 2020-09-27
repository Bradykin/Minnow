using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentInjuredTrollCard : GameCardEntityBase
{
    public ContentInjuredTrollCard()
    {
        m_entity = new ContentInjuredTroll();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Healing);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
