using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGolemProtectorCard : GameUnitCard
{
    public ContentGolemProtectorCard()
    {
        m_unit = new ContentGolemProtector();

        m_cost = 6;

        FillBasicData();
    }
}
