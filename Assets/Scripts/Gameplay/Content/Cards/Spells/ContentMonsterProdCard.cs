using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMonsterProdCard : GameCardSpellBase
{
    public ContentMonsterProdCard()
    {
        m_name = "Monster Prod";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;
        m_spellEffect = 1;

        m_keywordHolder.AddKeyword(new GameEnrageKeyword(null));

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Enrage);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.StaminaRegen);

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Target allied unit gains '<b>Enrage</b>: Gain {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} Stamina.'";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameEnrageKeyword(new GameGainStaminaAction(targetUnit, GetSpellValue())), false, false);
    }
}