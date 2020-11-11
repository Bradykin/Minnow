using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFrozenGuardianEnemyCard : GameUnitCard
{
    public ContentFrozenGuardianEnemyCard()
    {
        m_unit = new ContentFrozenGuardianEnemy(null);

        InitEnemyCard();
    }
}