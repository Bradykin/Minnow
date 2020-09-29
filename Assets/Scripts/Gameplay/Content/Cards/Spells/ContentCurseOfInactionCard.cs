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

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        return "Give a unit 'Momentum: take " + m_spellEffect + spString + " damage.'\n" + GetModifiedBySpellPowerString();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddKeyword(new GameMomentumKeyword(new GameGetHitAction(targetEntity, GetSpellValue())));
    }
}