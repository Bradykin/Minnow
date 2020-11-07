using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAlphaBoarCard : GameUnitCard
{
    public ContentAlphaBoarCard()
    {
        m_unit = new ContentAlphaBoar();

        m_cost = 2;

        FillBasicData();
    }
}
