using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRangerCard : GameCardEntityBase
{
    public ContentRangerCard()
    {
        m_entity = new ContentRanger();

        FillBasicData();

        m_playDesc = "DESC!";
        m_cost = 1;

        m_rarity = GameRarity.Common;
    }
}
