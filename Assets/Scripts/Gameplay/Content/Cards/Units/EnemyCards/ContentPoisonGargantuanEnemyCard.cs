using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentPoisonGargantuanEnemyCard : GameUnitCard
{
    public ContentPoisonGargantuanEnemyCard()
    {
        m_unit = new ContentPoisonGargantuanEnemy(null);

        InitEnemyCard();
    }
}