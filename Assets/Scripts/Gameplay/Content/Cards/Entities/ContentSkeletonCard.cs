using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSkeletonCard : GameCardEntityBase
{
    public ContentSkeletonCard()
    {
        m_entity = new ContentSkeleton();

        FillBasicData();

        m_playDesc = "It rises from the grave again and again...";
        m_cost = 1;
    }
}
