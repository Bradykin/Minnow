using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDeadeyeCard : GameCardSpellBase
{
    public ContentDeadeyeCard()
    {
        m_name = "Deadeye";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        m_cost = 3;

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        return "Target allied unit gains +1 range.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        if (targetUnit.GetRange() == 1)
        {
            targetUnit.AddKeyword(new GameRangeKeyword(2), false, false);
        }
        else
        {
            targetUnit.AddKeyword(new GameRangeKeyword(1), false, false);
        }
    }
}
