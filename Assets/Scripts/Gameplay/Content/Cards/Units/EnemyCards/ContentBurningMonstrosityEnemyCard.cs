using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBurningMonstrosityEnemyCard : GameUnitCard
{
    public ContentBurningMonstrosityEnemyCard()
    {
        m_unit = new ContentBurningMonstrosityEnemy(null);

        InitEnemyCard();
    }
}