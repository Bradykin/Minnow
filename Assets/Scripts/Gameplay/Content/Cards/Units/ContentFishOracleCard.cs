using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFishOracleCard : GameUnitCard
{
    public ContentFishOracleCard()
    {
        m_unit = new ContentFishOracle();

        m_cost = 1;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.MagicPower);
        m_tags.AddTag(GameTag.TagType.Shiv);
        m_tags.AddTag(GameTag.TagType.Scaler);
    }
}
