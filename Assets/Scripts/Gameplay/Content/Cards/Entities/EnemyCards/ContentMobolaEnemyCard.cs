using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMobolaEnemyCard : GameUnitCard
{
    public ContentMobolaEnemyCard()
    {
        m_unit = new ContentMobolaEnemy(null);

        InitEnemyCard();
    }
}