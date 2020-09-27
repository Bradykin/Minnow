using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGuardCaptainCard : GameCardEntityBase
{
    public ContentGuardCaptainCard()
    {
        m_entity = new ContentGuardCaptain();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.APRegen);
    }
}
