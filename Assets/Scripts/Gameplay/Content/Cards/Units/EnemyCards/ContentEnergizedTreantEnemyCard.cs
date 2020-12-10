using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEnergizedTreantEnemyCard : GameUnitCard
{
    public ContentEnergizedTreantEnemyCard()
    {
        m_unit = new ContentEnergizedTreantEnemy(null);

        InitEnemyCard();
    }
}