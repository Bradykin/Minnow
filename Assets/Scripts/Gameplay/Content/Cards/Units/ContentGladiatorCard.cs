using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGladiatorCard : GameUnitCard
{
    public ContentGladiatorCard()
    {
        m_unit = new ContentGladiator();

        m_cost = 2;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Enrage);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Tank);
    }
}
