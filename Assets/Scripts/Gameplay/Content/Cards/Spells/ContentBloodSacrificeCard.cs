using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBloodSacrificeCard : GameCardSpellBase
{
    public ContentBloodSacrificeCard()
    {
        m_spellEffect = 1;

        m_name = "Blood Sacrifice";
        m_targetType = Target.Ally;
        m_cost = 0;
        m_rarity = GameRarity.Rare;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Reanimate);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Knowledgeable);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower, 2);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilitySpell);

        m_onPlaySFX = AudioHelper.BloodSacrifice;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        if (GetSpellValue() > 1)
        {
            return $"Sacrifice an allied unit to draw {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} cards and gain {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} energy.";
        }
        else
        {
            return $"Sacrifice an allied unit to draw {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} card and gain {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} energy.";
        }
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.Die();

        GamePlayer player = GameHelper.GetPlayer();
        player.AddEnergy(GetSpellValue());
        player.DrawCards(GetSpellValue());
    }
}
