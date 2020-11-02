using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBasiliskEnemyCard : GameUnitCard
{
    public ContentBasiliskEnemyCard()
    {
        m_unit = new ContentBasiliskEnemy(null);

        InitEnemyCard();
    }
}