using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWingedSerpentCard : GameUnitCard
{
    public ContentWingedSerpentCard()
    {
        m_unit = new ContentWingedSerpent();

        m_cost = 1;

        FillBasicData();
    }
}
