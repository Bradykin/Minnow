using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentInfernalZombieEnemyCard : GameUnitCard
{
    public ContentInfernalZombieEnemyCard()
    {
        m_unit = new ContentInfernalZombieEnemy(null);

        InitEnemyCard();
    }
}