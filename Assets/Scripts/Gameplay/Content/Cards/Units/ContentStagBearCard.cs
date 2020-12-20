using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStagBearCard : GameUnitCard
{
    public ContentStagBearCard()
    {
        m_unit = new ContentStagBear();

        m_cost = 2;

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Momentum);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Enrage);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Victorious);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Scaler);

        FillBasicData();
    }
}
