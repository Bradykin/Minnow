using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLichEnemyCard : GameUnitCardBase
{
    public ContentLichEnemyCard()
    {
        m_unit = new ContentLichEnemy(null);

        InitEnemyCard();
    }
}