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

        m_cost = 2;

        m_keywordHolder.AddKeyword(new GameFadeKeyword());

        SetupBasicData();

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        return "Target allied unit gains Fade.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameFadeKeyword(), false, false);
    }
}
