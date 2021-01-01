using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFuryCard : GameCardSpellBase
{
    public ContentFuryCard()
    {
        m_name = "Fury";
        m_desc = "Trigger all instances of <b>Momentum</b>, <b>Enrage</b>, and <b>Victorious</b> on target allied unit.";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        m_keywordHolder.AddKeyword(new GameMomentumKeyword(null));
        m_keywordHolder.AddKeyword(new GameEnrageKeyword(null));
        m_keywordHolder.AddKeyword(new GameVictoriousKeyword(null));

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Momentum);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Enrage);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Victorious);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.Fury;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        GameMomentumKeyword momentumKeyword = targetUnit.GetMomentumKeyword();
        GameEnrageKeyword enrageKeyword = targetUnit.GetEnrageKeyword();
        GameVictoriousKeyword victoriousKeyword = targetUnit.GetVictoriousKeyword();

        if (momentumKeyword != null)
        {
            momentumKeyword.DoAction();
        }
        
        if (enrageKeyword != null)
        {
            enrageKeyword.DoAction();
        }

        if (victoriousKeyword != null)
        {
            victoriousKeyword.DoAction();
        }

        //Repeat action if the player has the Bestial Wrath Relic
        if (targetUnit.GetTypeline() == Typeline.Monster && targetUnit.GetTeam() == Team.Player)
        {
            if (GameHelper.HasRelic<ContentBestialWrathRelic>())
            {
                if (momentumKeyword != null)
                {
                    momentumKeyword.DoAction();
                }

                if (enrageKeyword != null)
                {
                    enrageKeyword.DoAction();
                }

                if (victoriousKeyword != null)
                {
                    victoriousKeyword.DoAction();
                }
                UIHelper.TriggerRelicAnimation<ContentBestialWrathRelic>();
            }
        }
    }
}