using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGreenRockGiantEnemyCard : GameUnitCard
{
    public ContentGreenRockGiantEnemyCard()
    {
        m_unit = new ContentGreenRockGiantEnemy(null);

        InitEnemyCard();
    }
}