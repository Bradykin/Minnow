using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentProductionCard : GameCardSpellBase
{
    private int m_stamRegenBuff = 1;

    public ContentProductionCard()
    {
        m_name = "Production";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        SetupBasicData();

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.StaminaRegen);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        return "Target allied unit <b>permanently</b> gains +" + m_stamRegenBuff + " stamina regen.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStaminaRegen(m_stamRegenBuff, true);
    }
}