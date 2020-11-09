using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRunicBladeCard : GameCardSpellBase
{
    public ContentRunicBladeCard()
    {
        m_name = "Runic Blade";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        m_playerUnlockLevel = 1;

        m_keywordHolder.AddKeyword(new GameVictoriousKeyword(null));
        m_keywordHolder.AddKeyword(new GameSpellcraftKeyword(null));

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Victorious);
        m_tags.AddTag(GameTag.TagType.Spellcraft);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        return "Target allied unit gains '<b>Victorious</b>: Trigger <b>Spellcraft</b> until end of wave.'";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameVictoriousKeyword(new GameSpellcraftAttackAction(targetUnit, 1)));
        GameHelper.GetPlayer().AddScheduledAction(ScheduledActionTime.EndOfWave, new GameLoseKeywordAction(targetUnit, new GameVictoriousKeyword(new GameSpellcraftAttackAction(targetUnit, 1))));
    }
}