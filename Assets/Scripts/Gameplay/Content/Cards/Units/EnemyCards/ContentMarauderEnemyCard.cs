using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarauderEnemyCard : GameUnitCard
{
    public ContentMarauderEnemyCard()
    {
        m_unit = new ContentMarauderEnemy(null);

        InitEnemyCard();
    }
}