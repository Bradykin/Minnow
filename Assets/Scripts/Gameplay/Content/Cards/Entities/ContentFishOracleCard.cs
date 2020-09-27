﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFishOracleCard : GameCardEntityBase
{
    public ContentFishOracleCard()
    {
        m_entity = new ContentFishOracle();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Spellpower);
        m_tags.AddTag(GameTag.TagType.Shiv);
        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
