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

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Reanimate);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilityUnit);
    }
}
