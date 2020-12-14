using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentStormChannelerCard : GameUnitCard
{
    public ContentStormChannelerCard()
    {
        m_unit = new ContentStormChanneler();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Spellcraft);
        m_tags.AddTag(GameTag.TagType.MagicPower);
        m_tags.AddTag(GameTag.TagType.LowCost);
    }
}
