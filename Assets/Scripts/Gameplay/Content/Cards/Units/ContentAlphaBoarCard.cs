using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAlphaBoarCard : GameUnitCard
{
    public ContentAlphaBoarCard()
    {
        InitializeWithLevel(GetCardLevel());

        m_unit = new ContentAlphaBoar();

        m_unit.InitializeWithLevel(GetCardLevel());

        m_cost = 2;

        FillBasicData();
    }
}
