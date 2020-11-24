using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWinterDragonEnemyCard : GameUnitCard
{
    public ContentWinterDragonEnemyCard()
    {
        m_unit = new ContentWinterDragonEnemy(null);

        InitEnemyCard();
    }
}