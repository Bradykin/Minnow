using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonSoldierCard : GameCardEntityBase
{
    public ContentDemonSoldierCard()
    {
        m_entity = new ContentDemonSoldier();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Explorer);
    }
}
