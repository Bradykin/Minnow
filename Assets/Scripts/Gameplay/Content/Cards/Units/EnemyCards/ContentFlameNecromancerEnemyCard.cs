using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFlameNecromancerEnemyCard : GameUnitCard
{
    public ContentFlameNecromancerEnemyCard()
    {
        m_unit = new ContentFlameNecromancerEnemy(null);

        InitEnemyCard();
    }
}