using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGuardCaptainCard : GameUnitCardBase
{
    public ContentGuardCaptainCard()
    {
        m_unit = new ContentGuardCaptain();

        m_cost = 2;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
    }
}
