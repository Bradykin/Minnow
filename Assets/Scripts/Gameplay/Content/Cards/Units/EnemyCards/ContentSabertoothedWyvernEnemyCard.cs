using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSabertoothWyvernEnemyCard : GameUnitCard
{
    public ContentSabertoothWyvernEnemyCard()
    {
        m_unit = new ContentSabertoothWyvernEnemy(null);

        InitEnemyCard();
    }
}