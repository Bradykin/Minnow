using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSprintCard : GameCardSpellBase
{
    private int m_stamRegenBuff = 1;

    public ContentSprintCard()
    {
        m_name = "Sprint";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        m_cost = 1;

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        return "Target allied unit gains +" + m_stamRegenBuff + " stamina regen.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStaminaRegen(m_stamRegenBuff, false);
    }
}
