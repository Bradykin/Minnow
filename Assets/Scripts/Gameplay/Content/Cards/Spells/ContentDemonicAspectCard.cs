using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonicAspectCard : GameCardSpellBase
{
    public ContentDemonicAspectCard()
    {
        m_name = "Demonic Aspect";
        m_desc = "Give target unit '<b>Victorious</b>: Gain 2 Stamina.'";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_shouldExile = true;

        m_rarity = GameRarity.Rare;

        m_keywordHolder.m_keywords.Add(new GameVictoriousKeyword(null));

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.Victorious);
        m_tags.AddTag(GameTag.TagType.Monster);
    }

    public override void PlayCard(GameUnit targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddKeyword(new GameVictoriousKeyword(new GameGainStaminaAction(targetEntity, 2)));
    }
}