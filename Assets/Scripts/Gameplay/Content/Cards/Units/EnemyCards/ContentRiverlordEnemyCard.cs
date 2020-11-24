using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRiverlordEnemyCard : GameUnitCard
{
    public ContentRiverlordEnemyCard()
    {
        m_unit = new ContentRiverlordEnemy(null);

        InitEnemyCard();
    }
}