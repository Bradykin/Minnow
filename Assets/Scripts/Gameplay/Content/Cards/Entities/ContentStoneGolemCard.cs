using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStoneGolemCard : GameUnitCardBase
{
    public ContentStoneGolemCard()
    {
        m_unit = new ContentStoneGolem();

        m_cost = 2;

        FillBasicData();
    }
}
