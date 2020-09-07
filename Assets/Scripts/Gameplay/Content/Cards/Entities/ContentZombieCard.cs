using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentZombieCard : GameCardEntityBase
{
    public ContentZombieCard()
    {
        m_entity = new ContentZombie();

        FillBasicData();

        m_playDesc = "A plague unpon this earth.";
        m_cost = 1;
    }
}
