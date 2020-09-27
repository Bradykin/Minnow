using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfShivcasterCard : GameCardEntityBase
{
    public ContentDwarfShivcasterCard()
    {
        m_entity = new ContentDwarfShivcaster();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Spellpower);
        m_tags.AddTag(GameTag.TagType.Shiv);
        m_tags.AddTag(GameTag.TagType.Midrange);
    }
}
