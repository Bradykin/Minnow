using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSiegebreakerCard : GameUnitCardBase
{
    public ContentSiegebreakerCard()
    {
        m_unit = new ContentSiegebreakerUnit(null);

        InitEnemyCard();
    }
}