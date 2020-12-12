using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBonecasterCard : GameUnitCard
{
    public ContentBonecasterCard()
    {
        m_unit = new ContentBonecaster();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.MagicPower);
        m_tags.AddTag(GameTag.TagType.Shiv);
        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
