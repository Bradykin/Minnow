using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFleshConstructEnemyCard : GameUnitCard
{
    public ContentFleshConstructEnemyCard()
    {
        m_unit = new ContentFleshConstructEnemy(null);

        InitEnemyCard();
    }
}