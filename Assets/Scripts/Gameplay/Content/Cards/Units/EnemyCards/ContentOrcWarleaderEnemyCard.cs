using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrcWarleaderEnemyCard : GameUnitCard
{
    public ContentOrcWarleaderEnemyCard()
    {
        m_unit = new ContentOrcWarleaderEnemy(null);

        InitEnemyCard();
    }
}