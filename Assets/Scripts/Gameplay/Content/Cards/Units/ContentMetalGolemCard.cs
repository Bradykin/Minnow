using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMetalGolemCard : GameUnitCard
{
    public ContentMetalGolemCard()
    {
        m_unit = new ContentMetalGolem();

        m_cost = 1;

        FillBasicData();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.DamageShield);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Midrange);
    }
}
