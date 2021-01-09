using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOverlordCard : GameUnitCard
{
    public ContentOverlordCard()
    {
        m_unit = new ContentOverlord();

        m_cost = 2;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.StaminaRegen);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.BuffSpell, 3);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Explorer);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Scaler);
    }
}
