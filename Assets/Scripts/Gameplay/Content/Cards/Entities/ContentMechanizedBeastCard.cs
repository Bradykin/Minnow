using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMechanizedBeastCard : GameUnitCard
{
    public ContentMechanizedBeastCard()
    {
        m_unit = new ContentMechanizedBeast();

        m_cost = 1;

        FillBasicData();
    }
}
