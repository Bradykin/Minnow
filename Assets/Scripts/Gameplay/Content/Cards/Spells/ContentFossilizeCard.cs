using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFossilizeCard : GameCardSpellBase
{
    public ContentFossilizeCard()
    {
        m_name = "Fossilize";
        m_desc = "Target enemy unit gains <b>Brittle</b>.";
        m_targetType = Target.Enemy;
        m_cost = 1;
        m_rarity = GameRarity.Rare;

        m_keywordHolder.AddKeyword(new GameBrittleKeyword());

        SetupBasicData();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilitySpell);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Brittle);

        m_onPlaySFX = AudioHelper.SmallDebuff;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameBrittleKeyword(), false, false);
    }
}
