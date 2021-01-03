using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLurkerEnemyCard : GameUnitCard
{
    public ContentLurkerEnemyCard()
    {
        m_unit = new ContentLurkerEnemy(null);

        InitEnemyCard();
    }
}