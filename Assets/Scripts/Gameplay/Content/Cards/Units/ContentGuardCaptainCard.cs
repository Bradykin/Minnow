using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGuardCaptainCard : GameUnitCard
{
    public ContentGuardCaptainCard()
    {
        m_unit = new ContentGuardCaptain();

        m_cost = 2;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Humanoid);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilityUnit);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.StaminaRegen);
    }
}
