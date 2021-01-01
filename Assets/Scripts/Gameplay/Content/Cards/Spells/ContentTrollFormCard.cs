using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTrollFormCard : GameCardSpellBase
{
    public ContentTrollFormCard()
    {
        m_spellEffect = 4;

        m_name = "Troll Form";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        m_keywordHolder.AddKeyword(new GameRegenerateKeyword(-1));

        SetupBasicData();

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.Healing);

        m_onPlaySFX = AudioHelper.Heal;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Target allied unit <b>permanently</b> gains '<b>Regen</b>: {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}'.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameRegenerateKeyword(GetSpellValue()), true, true);
    }
}