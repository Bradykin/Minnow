using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMageCard : GameCardEntityBase
{
    public ContentMageCard()
    {
        m_entity = new ContentMage();

        FillBasicData();

        m_playDesc = "DESC!";
        m_cost = 1;

        m_rarity = GameRarity.Common;
    }
}
