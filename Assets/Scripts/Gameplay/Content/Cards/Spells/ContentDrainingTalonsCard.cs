using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDrainingTalonsCard : GameCardSpellBase
{
    private int m_healVal = 5;

    public ContentDrainingTalonsCard()
    {
        m_name = "Draining Talons";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        m_keywordHolder.AddKeyword(new GameMomentumKeyword(null));

        m_cost = 3;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        return "Target allied unit gains '<b>Momentum</b>: Heal for " + m_healVal + ".";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameMomentumKeyword(new GameHealAction(targetUnit, m_healVal)), false, false);
    }
}
