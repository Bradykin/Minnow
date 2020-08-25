using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSabobotCard : GameCardEntityBase
{
    public ContentSabobotCard()
    {
        m_entity = new ContentSabobot();

        FillBasicData();

        m_playDesc = "One shot is all it needs!";
        m_cost = 1;
    }
}
