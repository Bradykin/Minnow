using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMetalGolemCard : GameCardEntityBase
{
    public ContentMetalGolemCard()
    {
        m_entity = new ContentMetalGolem();

        m_cost = 3;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Mountain);
        m_tags.AddTag(GameTag.TagType.DamageShield);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
