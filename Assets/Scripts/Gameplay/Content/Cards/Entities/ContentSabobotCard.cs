using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSabobotCard : GameUnitCard
{
    public ContentSabobotCard()
    {
        m_unit = new ContentSabobot();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Reanimate);
        m_tags.AddTag(GameTag.TagType.Explorer);
        m_tags.AddTag(GameTag.TagType.MaxStamina);
    }
}
