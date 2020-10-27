using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarvenSoldierCard : GameUnitCard
{
    public ContentDwarvenSoldierCard()
    {
        InitializeWithLevel(GetCardLevel());

        m_unit = new ContentDwarvenSoldier();

        m_unit.InitializeWithLevel(GetCardLevel());

        m_cost = 1;

        FillBasicData();
    }
}
