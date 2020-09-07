using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWildfolkCard : GameCardEntityBase
{
    public ContentWildfolkCard()
    {
        m_entity = new ContentWildfolk();

        FillBasicData();

        m_playDesc = "DESC!";
        m_cost = 1;

        m_rarity = GameRarity.Common;
    }
}
