using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLichEnemyCard : GameUnitCard
{
    public ContentLichEnemyCard()
    {
        m_unit = new ContentLichEnemy(null);

        InitEnemyCard();
    }
}