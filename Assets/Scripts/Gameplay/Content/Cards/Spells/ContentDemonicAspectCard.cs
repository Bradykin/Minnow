using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemonicAspectCard : GameCardSpellBase
{
    public ContentDemonicAspectCard()
    {
        m_name = "Demonic Aspect";
        m_desc = "Give target unit '<b>Victorious</b>: Gain 2 Stamina.'";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_shouldExile = true;
        m_spellEffect = 1;

        m_rarity = GameRarity.Rare;

        m_keywordHolder.AddKeyword(new GameVictoriousKeyword(null));

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Scaler);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.BuffSpell);
        m_tagHolder.AddPushTag(GameTagHolder.TagType.Victorious);
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

        return $"Give target unit '<b>Victorious</b>: Gain {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} Stamina.'";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameVictoriousKeyword(new GameGainStaminaAction(targetUnit, GetSpellValue())), false, false);
    }
}