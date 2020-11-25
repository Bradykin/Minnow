using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLavaHellionEnemyCard : GameUnitCard
{
    public ContentLavaHellionEnemyCard()
    {
        m_unit = new ContentLavaHellionEnemy(null);

        InitEnemyCard();
    }
}