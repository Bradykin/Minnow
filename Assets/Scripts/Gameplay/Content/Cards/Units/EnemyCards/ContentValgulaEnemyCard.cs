using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentValgulaEnemyCard : GameUnitCard
{
    public ContentValgulaEnemyCard()
    {
        m_unit = new ContentValgulaEnemy(null);

        InitEnemyCard();
    }
}