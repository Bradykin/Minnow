using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDarkMageEnemyCard : GameUnitCard
{
    public ContentDarkMageEnemyCard()
    {
        m_unit = new ContentDarkMageEnemy(null);

        InitEnemyCard();
    }
}