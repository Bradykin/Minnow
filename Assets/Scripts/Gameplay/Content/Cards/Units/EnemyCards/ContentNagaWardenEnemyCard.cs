using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNagaWardenEnemyCard : GameUnitCard
{
    public ContentNagaWardenEnemyCard()
    {
        m_unit = new ContentNagaWardenEnemy(null);

        InitEnemyCard();
    }
}