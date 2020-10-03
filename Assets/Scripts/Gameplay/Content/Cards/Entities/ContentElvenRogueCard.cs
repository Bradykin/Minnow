using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenRogueCard : GameUnitCardBase
{
    public ContentElvenRogueCard()
    {
        m_unit = new ContentElvenRogue();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
