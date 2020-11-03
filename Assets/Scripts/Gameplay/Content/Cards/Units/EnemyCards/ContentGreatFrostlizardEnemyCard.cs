using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGreatFrostlizardEnemyCard : GameUnitCard
{
    public ContentGreatFrostlizardEnemyCard()
    {
        m_unit = new ContentGreatFrostlizardEnemy(null);

        InitEnemyCard();
    }
}