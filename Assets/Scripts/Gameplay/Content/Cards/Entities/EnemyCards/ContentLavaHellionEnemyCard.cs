using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLavaHellionCard : GameUnitCard
{
    public ContentLavaHellionCard()
    {
        m_unit = new ContentLavaHellionEnemy(null);

        InitEnemyCard();
    }
}