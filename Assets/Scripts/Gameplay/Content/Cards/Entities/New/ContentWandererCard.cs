using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWandererCard : GameCardEntityBase
{
    public ContentWandererCard()
    {
        m_entity = new ContentWanderer();

        FillBasicData();

        m_playDesc = "DESC!";
        m_cost = 1;

        m_rarity = GameRarity.Common;
    }
}
