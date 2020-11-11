using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFireServantEnemyCard : GameUnitCard
{
    public ContentFireServantEnemyCard()
    {
        m_unit = new ContentFireServantEnemy(null);

        InitEnemyCard();
    }
}