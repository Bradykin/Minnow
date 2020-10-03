using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGladiatorCard : GameUnitCardBase
{
    public ContentGladiatorCard()
    {
        m_unit = new ContentGladiator();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Tank);
    }
}
