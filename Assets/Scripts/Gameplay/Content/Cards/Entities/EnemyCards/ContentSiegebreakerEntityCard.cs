using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSiegebreakerEntityCard : GameCardEntityBase
{
    public ContentSiegebreakerEntityCard()
    {
        m_entity = new ContentSiegebreakerEntity(null);

        InitEnemyCard();
    }
}