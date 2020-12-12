using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMetalProtectorCard : GameUnitCard
{
    public ContentMetalProtectorCard()
    {
        m_unit = new ContentMetalProtector();

        m_cost = 4;

        FillBasicData();
    }
}
