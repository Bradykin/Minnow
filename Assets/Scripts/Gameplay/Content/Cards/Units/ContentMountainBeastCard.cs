using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMountainBeastCard : GameUnitCard
{
    public ContentMountainBeastCard()
    {
        m_unit = new ContentMountainBeast();

        m_cost = 1;

        FillBasicData();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.Scaler);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Healing, isReceiver: false);
    }
}
