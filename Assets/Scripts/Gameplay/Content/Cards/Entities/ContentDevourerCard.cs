using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDevourerCard : GameUnitCardBase
{
    public ContentDevourerCard()
    {
        m_unit = new ContentDevourer();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
