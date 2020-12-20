using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFrogShamanCard : GameUnitCard
{
    public ContentFrogShamanCard()
    {
        m_unit = new ContentFrogShaman();

        m_cost = 3;

        FillBasicData();

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.Water);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.DamageSpell);
    }
}
