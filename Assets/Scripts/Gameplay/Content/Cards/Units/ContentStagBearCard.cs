using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStagBearCard : GameUnitCard
{
    public ContentStagBearCard()
    {
        m_unit = new ContentStagBear();

        m_cost = 1;

        FillBasicData();
    }
}
