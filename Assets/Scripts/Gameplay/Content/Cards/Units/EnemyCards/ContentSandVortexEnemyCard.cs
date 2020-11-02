using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSandVortexEnemyCard : GameUnitCard
{
    public ContentSandVortexEnemyCard()
    {
        m_unit = new ContentSandVortexEnemy(null);

        InitEnemyCard();
    }
}