using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStoneGolemCard : GameUnitCard
{
    public ContentStoneGolemCard()
    {
        SetCardLevel(GetCardLevel());

        m_unit = new ContentStoneGolem();

        m_unit.SetUnitLevel(m_cardLevel);

        m_cost = 2;

        FillBasicData();
    }
}
