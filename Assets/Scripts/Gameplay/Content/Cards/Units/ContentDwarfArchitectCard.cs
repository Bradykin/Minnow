using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDwarfArchitectCard : GameUnitCard
{
    public ContentDwarfArchitectCard()
    {
        m_unit = new ContentDwarfArchitect();

        m_cost = 2;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Creation, 3);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.StaminaRegen);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.MaxStamina);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilityUnit);
    }
}
