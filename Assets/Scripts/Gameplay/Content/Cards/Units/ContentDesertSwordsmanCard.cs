using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertSwordsmanCard : GameUnitCard
{
    public ContentDesertSwordsmanCard()
    {
        m_unit = new ContentDesertSwordsman();

        m_cost = 1;

        FillBasicData();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.Midrange);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.BuffSpell);
    }
}
