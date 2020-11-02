using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWildfolkCard : GameUnitCard
{
    public ContentWildfolkCard()
    {
        m_unit = new ContentWildfolk();

        m_cost = 2;
        m_playerUnlockLevel = 4;

        FillBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
        m_tags.AddTag(GameTag.TagType.Reanimate);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
    }
}
