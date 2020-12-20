using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRoyalCaravanCard : GameUnitCard
{
    public ContentRoyalCaravanCard()
    {
        m_unit = new ContentRoyalCaravan();

        m_cost = 3;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.BuffSpell);
    }
}
