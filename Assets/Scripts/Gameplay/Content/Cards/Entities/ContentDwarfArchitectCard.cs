using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfArchitectCard : GameCardEntityBase
{
    public ContentDwarfArchitectCard()
    {
        m_entity = new ContentDwarfArchitect();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.MaxStamina);
    }
}
