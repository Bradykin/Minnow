using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHomonculusCard : GameCardEntityBase
{
    public ContentHomonculusCard()
    {
        m_entity = new ContentHomonculus();

        FillBasicData();

        m_playDesc = "Are we sure this is a good idea?";
        m_cost = 1;
    }
}
