using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSpikewolfEnemyCard : GameUnitCard
{
    public ContentSpikewolfEnemyCard()
    {
        m_unit = new ContentSpikewolfEnemy(null);

        InitEnemyCard();
    }
}