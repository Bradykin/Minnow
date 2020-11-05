using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLivingBombCard : GameCardSpellBase
{
    private int m_range = 3;

    public ContentLivingBombCard()
    {
        m_spellEffect = 20;

        m_name = "Living Bomb";
        m_targetType = Target.Unit;
        m_cost = 2;
        m_rarity = GameRarity.Special;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.DamageSpell);
        m_tags.AddTag(GameTag.TagType.Spellpower);
        m_tags.AddTag(GameTag.TagType.Reanimate);
    }

    public override string GetDesc()
    {
        return "Target unit explodes on death, dealing " + GetSpellValue() + " damage to all units in range " + m_range + ".\n" + GetModifiedBySpellPowerString();
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        if (targetUnit.GetTeam() == Team.Player)
        {
            AudioHelper.PlaySFX(AudioHelper.SpellAudioCategory.Buff);
        }
        else
        {
            AudioHelper.PlaySFX(AudioHelper.SpellAudioCategory.Debuff);
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameDeathKeyword(new GameExplodeAction(targetUnit, GetSpellValue(), m_range)));
    }

    protected override void HandleAudio()
    {
        //Left blank intentionally, audio is handled in PlayCard
    }
}
