using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonSoldierCard : GameUnitCardBase
{
    public ContentDemonSoldierCard()
    {
        m_unit = new ContentDemonSoldier();

        m_cost = 3;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Explorer);
    }
}
