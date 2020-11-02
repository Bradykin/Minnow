using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSandWyvernEnemyCard : GameUnitCard
{
    public ContentSandWyvernEnemyCard()
    {
        m_unit = new ContentSandWyvernEnemy(null);

        InitEnemyCard();
    }
}