using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNoblemanCard : GameUnitCard
{
    public ContentNoblemanCard()
    {
        m_unit = new ContentNobleman();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Forest);
    }
}
