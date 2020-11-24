using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentUndeadFrostDragonEnemyCard : GameUnitCard
{
    public ContentUndeadFrostDragonEnemyCard()
    {
        m_unit = new ContentUndeadFrostDragonEnemy(null);

        InitEnemyCard();
    }
}