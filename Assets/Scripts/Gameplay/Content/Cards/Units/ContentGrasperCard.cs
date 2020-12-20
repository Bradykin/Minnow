using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrasperCard : GameUnitCard
{
    public ContentGrasperCard()
    {
        m_unit = new ContentGrasper();

        m_cost = 1;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MaxStamina);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilityUnit);
    }
}
