using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoCrabEnemyCard : GameUnitCard
{
    public ContentVolcanoCrabEnemyCard()
    {
        m_unit = new ContentVolcanoCrabEnemy(null);

        InitEnemyCard();
    }
}