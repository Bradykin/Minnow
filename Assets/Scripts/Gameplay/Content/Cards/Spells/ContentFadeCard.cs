using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFadeCard : GameCardSpellBase
{
    public ContentFadeCard()
    {
        m_name = "Fade";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Uncommon;

        m_cost = 1;

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        return "Target allied unit gains Fade until the beginning of next turn.\n<i>(Units with fade cannot be targeted with attacks, spells, or abilities by the opposing team.)</i>";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameFadeKeyword(), false, false);
        GameHelper.GetPlayer().AddScheduledAction(ScheduledActionTime.StartOfTurn, new GameLoseKeywordAction(targetUnit, new GameFadeKeyword()));
    }
}
