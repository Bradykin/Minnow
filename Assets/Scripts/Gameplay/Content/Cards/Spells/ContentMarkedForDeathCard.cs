using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarkedForDeathCard : GameCardSpellBase
{   
    public ContentMarkedForDeathCard()
    {
        m_spellEffect = 2;

        m_name = "Marked for Death";
        m_playDesc = "The target gets a jolt of energy!";
        m_targetType = Target.Enemy;
        m_cost = 3;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return "Target enemy gains Enrage: Gain Brittle " + GetSpellValue() + ".\n" + GetModifiedBySpellPowerString();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddKeyword(new GameEnrageKeyword(new GameGainBrittleAction(targetEntity, GetSpellValue())));
    }
}
