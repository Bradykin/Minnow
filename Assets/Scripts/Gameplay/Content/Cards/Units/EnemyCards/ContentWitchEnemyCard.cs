using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWitchEnemyCard : GameUnitCard
{
    public ContentWitchEnemyCard()
    {
        m_unit = new ContentWitchEnemy(null);

        InitEnemyCard();
    }
}