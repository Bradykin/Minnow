using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLizardmanEnemyCard : GameUnitCard
{
    public ContentLizardmanEnemyCard()
    {
        m_unit = new ContentLizardmanEnemy(null);

        InitEnemyCard();
    }
}