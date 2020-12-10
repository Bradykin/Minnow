using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStormChannelerCard : GameUnitCard
{
    public ContentStormChannelerCard()
    {
        m_unit = new ContentStormChanneler();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Forest);
    }
}
