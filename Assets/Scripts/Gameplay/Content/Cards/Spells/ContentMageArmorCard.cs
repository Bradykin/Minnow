using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMageArmorCard : GameCardSpellBase
{
    public ContentMageArmorCard()
    {
        m_name = "Mage Armor";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;
        m_spellEffect = 2;

        m_cost = 0;

        SetupBasicData();

        m_keywordHolder.AddKeyword(new GameDamageReductionKeyword(0));

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Target allied unit gains <b>Damage Reduction</b> {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameDamageReductionKeyword(GetSpellValue()), false, false);
    }
}
