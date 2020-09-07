using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGuardCaptainCard : GameCardEntityBase
{
    public ContentGuardCaptainCard()
    {
        m_entity = new ContentGuardCaptain();

        FillBasicData();

        m_playDesc = "With a single shout, the troops respond with renewed vigor!";
        m_cost = 2;
    }
}
