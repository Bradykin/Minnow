using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHomonculusCard : GameCardEntityBase
{
    public ContentHomonculusCard()
    {
        m_entity = new ContentHomonculus();

        FillBasicData();

        m_playDesc = "DESC!";
        m_cost = 1;
    }
}
