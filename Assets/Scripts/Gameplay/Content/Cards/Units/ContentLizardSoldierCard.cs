using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLizardSoldierCard : GameUnitCard
{
    public ContentLizardSoldierCard()
    {
        m_unit = new ContentLizardSoldier();

        m_cost = 2;

        FillBasicData();
    }
}
