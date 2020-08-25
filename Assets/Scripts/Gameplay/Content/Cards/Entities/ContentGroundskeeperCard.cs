using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGroundskeeperCard : GameCardEntityBase
{
    public ContentGroundskeeperCard()
    {
        m_entity = new ContentGroundskeeper();

        FillBasicData();

        m_playDesc = "A mighty tree; unwavering in its defense.";
        m_cost = 1;
    }
}
