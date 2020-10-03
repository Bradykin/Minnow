using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMobolaEnemyCard : GameUnitCardBase
{
    public ContentMobolaEnemyCard()
    {
        m_unit = new ContentMobolaEnemy(null);

        InitEnemyCard();
    }
}