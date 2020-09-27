using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarvenSoldierCard : GameCardEntityBase
{
    public ContentDwarvenSoldierCard()
    {
        m_entity = new ContentDwarvenSoldier();

        m_cost = 1;

        FillBasicData();
    }
}
