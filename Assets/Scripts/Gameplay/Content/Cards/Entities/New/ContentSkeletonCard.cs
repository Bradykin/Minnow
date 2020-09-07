using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSkeletonCard : GameCardEntityBase
{
    public ContentSkeletonCard()
    {
        m_entity = new ContentSkeleton();

        FillBasicData();

        m_playDesc = "DESC!";
        m_cost = 1;

        m_rarity = GameRarity.Common;
    }
}
