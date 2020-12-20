using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRoarOfVictoryCard : GameCardSpellBase
{
    public ContentRoarOfVictoryCard()
    {
        m_name = "Roar of Victory";
        m_desc = "Target allied <b>Monster</b> unit gains '<b>Victorious</b>: Trigger all <b>Momentum</b> and <b>Enrage</b> keywords on this unit'.";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        m_keywordHolder.AddKeyword(new GameVictoriousKeyword(null));
        m_keywordHolder.AddKeyword(new GameMomentumKeyword(null));
        m_keywordHolder.AddKeyword(new GameEnrageKeyword(null));

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Victorious);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Momentum);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Enrage);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Monster);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override bool IsValidToPlay(GameUnit targetUnit)
    {
        return base.IsValidToPlay(targetUnit) && targetUnit.GetTypeline() == Typeline.Monster;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameVictoriousKeyword(new GameRoarOfVictoryAction(targetUnit, 1)), false, false);
    }
}