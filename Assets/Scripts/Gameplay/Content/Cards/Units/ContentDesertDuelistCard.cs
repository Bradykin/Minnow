using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertDuelistCard : GameUnitCard
{
    public ContentDesertDuelistCard()
    {
        m_unit = new ContentDesertDuelist();

        m_cost = 1;

        FillBasicData();
    }
}
