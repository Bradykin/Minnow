using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarvenSoldierCard : GameCardEntityBase
{
    public ContentDwarvenSoldierCard()
    {
        m_entity = new ContentDwarvenSoldier();

        FillBasicData();

        m_playDesc = "Hi ho!  Hi ho, it's off to work we go!";
        m_cost = 1;
    }
}
