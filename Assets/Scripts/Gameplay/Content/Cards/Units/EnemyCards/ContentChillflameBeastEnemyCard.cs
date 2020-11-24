using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentChillflameBeastEnemyCard : GameUnitCard
{
    public ContentChillflameBeastEnemyCard()
    {
        m_unit = new ContentChillflameBeastEnemy(null);

        InitEnemyCard();
    }
}