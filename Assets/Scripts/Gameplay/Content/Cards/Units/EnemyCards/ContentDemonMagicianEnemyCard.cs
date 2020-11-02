using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonMagicianEnemyCard : GameUnitCard
{
    public ContentDemonMagicianEnemyCard()
    {
        m_unit = new ContentDemonMagicianEnemy(null);

        InitEnemyCard();
    }
}