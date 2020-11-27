using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGuardianOfTheForestCard : GameUnitCard
{
    public ContentGuardianOfTheForestCard()
    {
        m_unit = new ContentGuardianOfTheForest();

        m_cost = 1;

        FillBasicData();
    }
}
