using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHomonculusCard : GameCardEntityBase
{
    public ContentHomonculusCard()
    {
        m_entity = new ContentHomonculus();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
    }
}
