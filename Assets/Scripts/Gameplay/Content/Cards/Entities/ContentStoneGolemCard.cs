using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStoneGolemCard : GameUnitCard
{
    public ContentStoneGolemCard()
    {
        m_unit = new ContentStoneGolem();

        m_cost = 2;

        FillBasicData();
    }
}
