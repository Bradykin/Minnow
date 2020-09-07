using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGuardCaptainCard : GameCardEntityBase
{
    public ContentGuardCaptainCard()
    {
        m_entity = new ContentGuardCaptain();

        FillBasicData();

        m_playDesc = "DESC!";
        m_cost = 1;
    }
}
