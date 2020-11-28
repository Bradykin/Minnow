using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFrostGiantEnemyCard : GameUnitCard
{
    public ContentFrostGiantEnemyCard()
    {
        m_unit = new ContentFrostGiantEnemy(null);

        InitEnemyCard();
    }
}