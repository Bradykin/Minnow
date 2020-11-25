using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSkeletalCaptainEnemyCard : GameUnitCard
{
    public ContentSkeletalCaptainEnemyCard()
    {
        m_unit = new ContentSkeletalCaptainEnemy(null);

        InitEnemyCard();
    }
}