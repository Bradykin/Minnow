using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSandwalkerCard : GameUnitCard
{
    public ContentSandwalkerCard()
    {
        m_unit = new ContentSandwalker();

        m_cost = 1;

        FillBasicData();
    }
}
