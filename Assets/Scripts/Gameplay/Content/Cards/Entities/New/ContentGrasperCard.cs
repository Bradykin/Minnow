using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrasperCard : GameCardEntityBase
{
    public ContentGrasperCard()
    {
        m_entity = new ContentGrasper();

        FillBasicData();

        m_playDesc = "DESC!";
        m_cost = 1;

        m_rarity = GameRarity.Common;
    }
}
