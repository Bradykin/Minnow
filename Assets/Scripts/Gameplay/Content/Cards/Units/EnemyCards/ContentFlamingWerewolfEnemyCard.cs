using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFlamingWerewolfEnemyCard : GameUnitCard
{
    public ContentFlamingWerewolfEnemyCard()
    {
        m_unit = new ContentFlamingWerewolfEnemy(null);

        InitEnemyCard();
    }
}