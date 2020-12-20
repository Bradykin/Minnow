using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShivCard : GameCardSpellBase
{
    private bool m_canTriggerSpellcraft = true;

    public ContentShivCard()
    {
        m_spellEffect = 4;

        m_name = "Shiv";
        m_targetType = Target.Enemy;
        m_cost = 0;
        m_rarity = GameRarity.Special;
        m_shouldExile = true;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.DaggerSwingSpell;
    }

    public override string GetDesc()
    {
        return GetDamageDescString();
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        if (GameHelper.HasRelic<ContentPoisonedShivsRelic>())
        {
            targetUnit.SpendStamina(2);
        }

        if (GameHelper.HasRelic<ContentBurningShivsRelic>())
        {
            for (int i = 0; i < 3; i++)
            {
                if (!targetUnit.m_isDead)
                {
                    base.PlayCard(targetUnit);
                    targetUnit.GetHitBySpell(GetSpellValue(), this);
                }
            }
        }
        else
        {
            base.PlayCard(targetUnit);
            targetUnit.GetHitBySpell(GetSpellValue(), this);
        }
    }

    public void SetCanTriggerSpellcraft(bool newVal)
    {
        m_canTriggerSpellcraft = newVal;
    }

    protected override bool CanTriggerSpellcraft()
    {
        return m_canTriggerSpellcraft;
    }
}
