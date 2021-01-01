using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDrainingTalonsCard : GameCardSpellBase
{
    public ContentDrainingTalonsCard()
    {
        m_name = "Draining Talons";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;
        m_spellEffect = 3;

        m_keywordHolder.AddKeyword(new GameMomentumKeyword(null));

        m_cost = 3;

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

        return $"Target allied unit gains '<b>Momentum</b>: Heal for {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameMomentumKeyword(new GameHealAction(targetUnit, GetSpellValue())), false, false);
    }
}
