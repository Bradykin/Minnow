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
        return "All enemies ignore target allied unit until the end of turn.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        //Alex: Make this do the thing
    }
}
