using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStoneflingerEnemyCard : GameUnitCard
{
    public ContentStoneflingerEnemyCard()
    {
        m_unit = new ContentStoneflingerEnemy(null);

        InitEnemyCard();
    }
}