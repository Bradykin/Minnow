using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCurseOfInactionCard : GameCardSpellBase
{
    public ContentCurseOfInactionCard()
    {
        m_spellEffect = 1;

        m_name = "Curse of Inaction";
        m_targetType = Target.Unit;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        m_keywordHolder.AddKeyword(new GameMomentumKeyword(null));

        SetupBasicData();

        m_onPlaySFX = AudioHelper.SmallDebuff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Give a unit '<b>Momentum</b>: take " + m_spellEffect + mpString + " damage.'\n" + GetModifiedByMagicPowerString();
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameMomentumKeyword(new GameGetHitAction(targetUnit, GetSpellValue())), false, false);
    }
}