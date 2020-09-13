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
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddKeyword(new GameRegenerateKeyword(m_regenNum));
    }
}