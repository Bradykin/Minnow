using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonSoldierCard : GameCardEntityBase
{
    public ContentDemonSoldierCard()
    {
        m_entity = new ContentDemonSoldier();

        m_cost = 3;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Explorer);
    }
}
