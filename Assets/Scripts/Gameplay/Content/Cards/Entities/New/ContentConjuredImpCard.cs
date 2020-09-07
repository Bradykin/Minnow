using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentConjuredImpCard : GameCardEntityBase
{
    public ContentConjuredImpCard()
    {
        m_entity = new ContentConjuredImp();

        FillBasicData();

        m_playDesc = "DESC!";
        m_cost = 1;

        m_rarity = GameRarity.Common;
    }
}
