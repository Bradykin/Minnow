using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOverlordCard : GameCardEntityBase
{
    public ContentOverlordCard()
    {
        m_entity = new ContentOverlord();

        FillBasicData();

        m_playDesc = "DESC!";
        m_cost = 1;

        m_rarity = GameRarity.Common;
    }
}
