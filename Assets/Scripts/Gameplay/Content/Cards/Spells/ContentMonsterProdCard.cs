using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMonsterProdCard : GameCardSpellBase
{
    public ContentMonsterProdCard()
    {
        m_name = "Monster Prod";
        m_desc = "Target allied <b>Monster</b> unit gains '<b>Enrage</b>: Gain 1 Stamina.'";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        m_playerUnlockLevel = 2;

        m_keywordHolder.AddKeyword(new GameEnrageKeyword(null));

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Enrage);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.Healing);
    }

    public override bool IsValidToPlay(GameUnit targetUnit)
    {
        return base.IsValidToPlay() && targetUnit.GetTypeline() == Typeline.Monster;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameEnrageKeyword(new GameGainStaminaAction(targetUnit, 1)));
    }
}