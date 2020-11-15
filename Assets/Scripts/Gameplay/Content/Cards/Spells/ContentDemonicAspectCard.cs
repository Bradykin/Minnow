using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonicAspectCard : GameCardSpellBase
{
    public ContentDemonicAspectCard()
    {
        m_name = "Demonic Aspect";
        m_desc = "Give target unit '<b>Victorious</b>: Gain 2 Stamina until end of wave.'";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_shouldExile = true;

        m_rarity = GameRarity.Rare;

        m_keywordHolder.AddKeyword(new GameVictoriousKeyword(null));

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Scaler);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.Victorious);
        m_tags.AddTag(GameTag.TagType.Monster);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameVictoriousKeyword(new GameGainStaminaAction(targetUnit, 2)));
        GameHelper.GetPlayer().AddScheduledAction(ScheduledActionTime.EndOfWave, new GameLoseKeywordAction(targetUnit, new GameVictoriousKeyword(new GameGainStaminaAction(targetUnit, 2))));
    }
}