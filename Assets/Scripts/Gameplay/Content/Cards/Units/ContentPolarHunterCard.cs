using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPolarHunterCard : GameUnitCard
{
    public ContentPolarHunterCard()
    {
        m_unit = new ContentPolarHunter();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }
}
