using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFishOracleCard : GameCardEntityBase
{
    public ContentFishOracleCard()
    {
        m_entity = new ContentFishOracle();

        FillBasicData();

        m_playDesc = "Reading the bones tells either truths or lies...";
        m_cost = 2;
    }
}
