using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarvenSoldierCard : GameUnitCard
{
    public ContentDwarvenSoldierCard()
    {
        SetCardLevel(GamePlayer.DwarvenLevel);

        m_unit = new ContentDwarvenSoldier();

        m_unit.SetUnitLevel(m_cardLevel);

        m_cost = 1;

        FillBasicData();
    }
}
