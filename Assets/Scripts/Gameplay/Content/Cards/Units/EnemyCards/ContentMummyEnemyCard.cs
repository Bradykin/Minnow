using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMummyEnemyCard : GameUnitCard
{
    public ContentMummyEnemyCard()
    {
        m_unit = new ContentMummyEnemy(null);

        InitEnemyCard();
    }
}