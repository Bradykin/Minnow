using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSkeletonCard : GameCardEntityBase
{
    public ContentSkeletonCard()
    {
        m_entity = new ContentSkeleton();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Midrange);
        m_tags.AddTag(GameTag.TagType.Reanimate);
        m_tags.AddTag(GameTag.TagType.Healing);
    }
}
