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

        m_keywordHolder.m_keywords.Add(new GameMomentumKeyword(null));

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        return "Give a unit '<b>Momentum</b>: take " + m_spellEffect + spString + " damage.'\n" + GetModifiedBySpellPowerString();
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