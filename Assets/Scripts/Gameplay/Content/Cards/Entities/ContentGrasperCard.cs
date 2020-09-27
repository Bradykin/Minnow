using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrasperCard : GameCardEntityBase
{
    public ContentGrasperCard()
    {
        m_entity = new ContentGrasper();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.APRegen);
        m_tags.AddTag(GameTag.TagType.MaxAP);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Tank);
    }
}
