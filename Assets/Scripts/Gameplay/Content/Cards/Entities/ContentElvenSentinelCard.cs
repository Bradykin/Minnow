using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenSentinelCard : GameCardEntityBase
{
    public ContentElvenSentinelCard()
    {
        m_entity = new ContentElvenSentinel();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.APRegen);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
