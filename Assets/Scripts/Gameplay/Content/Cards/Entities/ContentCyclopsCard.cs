using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCyclopsCard : GameUnitCardBase
{
    public ContentCyclopsCard()
    {
        m_unit = new ContentCyclops();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
