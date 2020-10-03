using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMetalGolemCard : GameUnitCardBase
{
    public ContentMetalGolemCard()
    {
        m_unit = new ContentMetalGolem();

        m_cost = 3;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Mountain);
        m_tags.AddTag(GameTag.TagType.DamageShield);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
