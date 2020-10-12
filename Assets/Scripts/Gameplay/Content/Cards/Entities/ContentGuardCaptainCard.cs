using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGuardCaptainCard : GameUnitCard
{
    public ContentGuardCaptainCard()
    {
        m_unit = new ContentGuardCaptain();

        m_cost = 2;
        m_playerUnlockLevel = 3;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
    }
}
