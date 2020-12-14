using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStagBearCard : GameUnitCard
{
    public ContentStagBearCard()
    {
        m_unit = new ContentStagBear();

        m_cost = 2;

        m_tags.AddTag(GameTag.TagType.Scaler);
        m_tags.AddTag(GameTag.TagType.BuffSpell);

        FillBasicData();
    }
}
