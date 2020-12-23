using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMetalProtectorCard : GameUnitCard
{
    public ContentMetalProtectorCard()
    {
        m_unit = new ContentMetalProtector();

        m_cost = 3;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MaxStamina, 2);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Scaler);
    }
}
