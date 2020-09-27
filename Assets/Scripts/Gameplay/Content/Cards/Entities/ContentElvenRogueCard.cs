using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenRogueCard : GameCardEntityBase
{
    public ContentElvenRogueCard()
    {
        m_entity = new ContentElvenRogue();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
