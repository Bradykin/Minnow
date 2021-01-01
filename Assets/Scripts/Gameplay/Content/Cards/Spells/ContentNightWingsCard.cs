using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNightWingsCard : GameCardSpellBase
{
    public ContentNightWingsCard()
    {
        m_name = "Night Wings";
        m_desc = "Give a friendly unit <b>Flying</b>.";
        m_targetType = Target.Ally;
        m_cost = 4;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        m_keywordHolder.AddKeyword(new GameFlyingKeyword());

        SetupBasicData();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.Explorer);

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameFlyingKeyword(), false, false);
    }
}