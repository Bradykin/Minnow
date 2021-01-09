using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentArcaneBoltCard : GameCardSpellBase
{
    public ContentArcaneBoltCard()
    {
        m_spellEffect = 6;

        m_name = "Arcane Bolt";
        m_targetType = Target.Enemy;
        m_cost = 0;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower, 3);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Spellcraft);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.DamageSpell);

        m_onPlaySFX = AudioHelper.MagicBolt;
    }

    public override string GetDesc()
    {
        return GetDamageDescString() + "x3 benefits from <b>Magic Power</b>.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.GetHitBySpell(GetSpellValue(), this);
    }

    protected override int GetSpellValue()
    {
        int spellValueBase = base.GetSpellValue() - m_spellEffect;

        spellValueBase = 3 * spellValueBase + m_spellEffect;

        if (spellValueBase < 0)
        {
            spellValueBase = 0;
        }

        return spellValueBase;
    }
}
