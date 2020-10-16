using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentUndeadMammothCard : GameUnitCard
{
    public ContentUndeadMammothCard()
    {
        m_unit = new ContentUndeadMammoth();

        m_cost = 2;

        FillBasicData();
    }
}
