using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSkeletonCard : GameUnitCard
{
    public ContentSkeletonCard()
    {
        m_unit = new ContentSkeleton();

        m_cost = 1;
        m_playerUnlockLevel = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Reanimate);
        m_tags.AddTag(GameTag.TagType.Healing);
    }
}
