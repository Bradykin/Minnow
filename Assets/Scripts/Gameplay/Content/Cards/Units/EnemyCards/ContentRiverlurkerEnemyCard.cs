using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRiverlurkerEnemyCard : GameUnitCard
{
    public ContentRiverlurkerEnemyCard()
    {
        m_unit = new ContentRiverlurkerEnemy(null);

        InitEnemyCard();
    }
}