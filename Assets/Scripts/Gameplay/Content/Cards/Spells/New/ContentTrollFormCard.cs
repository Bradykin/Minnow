using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTrollFormCard : GameCardSpellBase
{
    private int m_regenNum = 4;

    public ContentTrollFormCard()
    {
        m_name = "Troll Form";
        m_desc = "Give an entity regenrate " + m_regenNum + ".";
        m_playDesc = "The target begins to regenerate!";
        m_targetType = Target.Ally;
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

        targetEntity.GetKeywordHolder().m_keywords.Add(new GameRegenerateKeyword(m_regenNum));
    }
}