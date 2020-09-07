using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMetalGolemCard : GameCardEntityBase
{
    public ContentMetalGolemCard()
    {
        m_entity = new ContentMetalGolem();

        FillBasicData();

        m_playDesc = "DESC!";
        m_cost = 1;

        m_rarity = GameRarity.Common;
    }
}
