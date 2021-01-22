using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLordOfFrostEnemyCard : GameUnitCard
{
    public ContentLordOfFrostEnemyCard()
    {
        m_unit = new ContentLordOfFrostEnemy(null);

        InitEnemyCard();
    }
}