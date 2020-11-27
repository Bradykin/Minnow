using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSkeletalPirateEnemyCard : GameUnitCard
{
    public ContentSkeletalPirateEnemyCard()
    {
        m_unit = new ContentSkeletalPirateEnemy(null);

        InitEnemyCard();
    }
}