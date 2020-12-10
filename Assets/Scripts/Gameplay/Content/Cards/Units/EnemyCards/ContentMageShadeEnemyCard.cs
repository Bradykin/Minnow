using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMageShadeEnemyCard : GameUnitCard
{
    public ContentMageShadeEnemyCard()
    {
        m_unit = new ContentMageShadeEnemy(null);

        InitEnemyCard();
    }
}