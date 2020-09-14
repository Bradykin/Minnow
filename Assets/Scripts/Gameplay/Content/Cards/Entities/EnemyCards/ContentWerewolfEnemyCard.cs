using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWerewolfEnemyCard : GameCardEntityBase
{
    public ContentWerewolfEnemyCard()
    {
        m_entity = new ContentWerewolfEnemy(null);

        InitEnemyCard();
    }
}