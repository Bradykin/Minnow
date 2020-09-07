using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMinerCard : GameCardEntityBase
{
    public ContentMinerCard()
    {
        m_entity = new ContentMiner();

        FillBasicData();

        m_playDesc = "DESC!";
        m_cost = 1;
    }
}
