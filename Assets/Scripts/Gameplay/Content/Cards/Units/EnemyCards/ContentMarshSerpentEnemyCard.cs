using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarshSerpentEnemyCard : GameUnitCard
{
    public ContentMarshSerpentEnemyCard()
    {
        m_unit = new ContentMarshSerpentEnemy(null);

        InitEnemyCard();
    }
}