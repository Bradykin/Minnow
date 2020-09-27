using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSabobotCard : GameCardEntityBase
{
    public ContentSabobotCard()
    {
        m_entity = new ContentSabobot();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Reanimate);
        m_tags.AddTag(GameTag.TagType.Explorer);
        m_tags.AddTag(GameTag.TagType.MaxAP);
    }
}
