using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSpinnerEnemyCard : GameUnitCardBase
{
    public ContentSpinnerEnemyCard()
    {
        m_unit = new ContentSpinnerEnemy(null);

        InitEnemyCard();
    }
}