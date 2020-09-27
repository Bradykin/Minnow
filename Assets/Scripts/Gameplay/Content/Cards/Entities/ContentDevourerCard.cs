using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDevourerCard : GameCardEntityBase
{
    public ContentDevourerCard()
    {
        m_entity = new ContentDevourer();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
