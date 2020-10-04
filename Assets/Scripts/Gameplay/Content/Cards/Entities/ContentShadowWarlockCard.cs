using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShadowWarlockCard : GameUnitCard
{
    public ContentShadowWarlockCard()
    {
        m_unit = new ContentShadowWarlock();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
    }
}
