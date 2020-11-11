using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHuskEnemyCard : GameUnitCard
{
    public ContentHuskEnemyCard()
    {
        m_unit = new ContentHuskEnemy(null);

        InitEnemyCard();
    }
}