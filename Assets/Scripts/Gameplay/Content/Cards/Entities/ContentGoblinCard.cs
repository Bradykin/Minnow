using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoblinCard : GameUnitCardBase
{
    public ContentGoblinCard()
    {
        m_unit = new ContentGoblin();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
