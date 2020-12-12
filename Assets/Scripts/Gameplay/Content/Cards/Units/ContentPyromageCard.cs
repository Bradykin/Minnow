using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPyromageCard : GameUnitCard
{
    public ContentPyromageCard()
    {
        m_unit = new ContentPyromage();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.DamageSpell);
    }
}
