using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBlindBeastEnemyCard : GameUnitCard
{
    public ContentBlindBeastEnemyCard()
    {
        m_unit = new ContentBlindBeastEnemy(null);

        InitEnemyCard();
    }
}