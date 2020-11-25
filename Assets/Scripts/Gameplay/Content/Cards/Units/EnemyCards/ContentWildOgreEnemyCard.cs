using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWildOgreEnemyCard : GameUnitCard
{
    public ContentWildOgreEnemyCard()
    {
        m_unit = new ContentWildOgreEnemy(null);

        InitEnemyCard();
    }
}