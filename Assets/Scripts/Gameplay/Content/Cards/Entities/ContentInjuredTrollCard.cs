using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentInjuredTrollCard : GameCardEntityBase
{
    public ContentInjuredTrollCard()
    {
        m_entity = new ContentInjuredTrollEntity();

        FillBasicData();

        m_playDesc = "This troll may be injured for now, but it will soon be full strength and mighty!";
        m_cost = 2;
    }
}
