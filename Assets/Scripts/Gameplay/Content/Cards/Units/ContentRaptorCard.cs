using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRaptorCard : GameUnitCard
{
    public ContentRaptorCard()
    {
        m_unit = new ContentRaptor();

        m_cost = 1;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.StaminaRegen);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Scaler);
    }
}
