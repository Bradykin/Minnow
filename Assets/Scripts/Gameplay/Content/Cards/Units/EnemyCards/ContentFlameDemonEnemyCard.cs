using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFlameDemonEnemyCard : GameUnitCard
{
    public ContentFlameDemonEnemyCard()
    {
        m_unit = new ContentFlameDemonEnemy(null);

        InitEnemyCard();
    }
}