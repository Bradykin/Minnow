using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMummyPharaohEnemyCard : GameUnitCard
{
    public ContentMummyPharaohEnemyCard()
    {
        m_unit = new ContentMummyPharaohEnemy(null);

        InitEnemyCard();
    }
}