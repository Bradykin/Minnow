using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentElvenRogueCard : GameUnitCard
{
    public ContentElvenRogueCard()
    {
        m_unit = new ContentElvenRogue();

        m_cost = 1;

        FillBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Scaler);
    }
}
