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

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Shiv);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Scaler);
    }
}
