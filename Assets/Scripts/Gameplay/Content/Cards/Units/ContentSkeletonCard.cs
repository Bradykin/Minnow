using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSkeletonCard : GameUnitCard
{
    public ContentSkeletonCard()
    {
        m_unit = new ContentSkeleton();

        m_cost = 1;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Reanimate);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Scaler);
    }
}
