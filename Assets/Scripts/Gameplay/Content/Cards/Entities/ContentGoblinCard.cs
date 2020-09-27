using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGoblinCard : GameCardEntityBase
{
    public ContentGoblinCard()
    {
        m_entity = new ContentGoblin();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
