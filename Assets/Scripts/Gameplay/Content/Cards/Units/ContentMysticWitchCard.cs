using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMysticWitchCard : GameUnitCard
{
    public ContentMysticWitchCard()
    {
        m_unit = new ContentMysticWitch();

        m_cost = 3;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.LowCost);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Knowledgeable);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilityUnit);
    }
}
