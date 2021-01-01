using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSprintCard : GameCardSpellBase
{
    public ContentSprintCard()
    {
        m_name = "Sprint";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;
        m_spellEffect = 1;

        m_cost = 1;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Target allied unit gains +{UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} stamina regen.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStaminaRegen(GetSpellValue(), false);
    }
}
