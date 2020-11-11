using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLordOfShadowsEnemyCard : GameUnitCard
{
    public ContentLordOfShadowsEnemyCard()
    {
        m_unit = new ContentLordOfShadowsEnemy(null);

        InitEnemyCard();
    }
}