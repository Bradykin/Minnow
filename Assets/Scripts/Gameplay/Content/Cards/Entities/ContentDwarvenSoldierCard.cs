using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarvenSoldierCard : GameUnitCard
{
    public ContentDwarvenSoldierCard()
    {
        m_unit = new ContentDwarvenSoldier();

        m_cost = 1;

        FillBasicData();
    }
}
