using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDrainCard : GameCardSpellBase
{
    public ContentDrainCard()
    {
        m_name = "Drain";
        m_desc = "Set an entities AP to 0.";
        m_playDesc = "The target gets drained!";
        m_targetType = Target.Entity;
        m_typeline = "Spell - " + m_targetType;
        m_cost = 1;
        m_icon = UIHelper.GetIconCard(m_name);
        m_rarity = GameRarity.Uncommon;
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.EmptyAP();
    }
}
