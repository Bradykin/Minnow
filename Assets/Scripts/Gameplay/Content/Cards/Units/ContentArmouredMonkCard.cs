using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentArmouredMonkCard : GameUnitCard
{
    public ContentArmouredMonkCard()
    {
        m_unit = new ContentArmouredMonk();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Forest);
    }
}
