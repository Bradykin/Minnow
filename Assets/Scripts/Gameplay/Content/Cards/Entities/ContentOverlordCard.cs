using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOverlordCard : GameCardEntityBase
{
    public ContentOverlordCard()
    {
        m_entity = new ContentOverlord();

        FillBasicData();

        m_playDesc = "So many eyeballs...";
        m_cost = 2;
    }
}
