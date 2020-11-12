using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFireboltCard : GameCardSpellBase
{
    public ContentFireboltCard()
    {
        m_name = "Firebolt";
        m_targetType = Target.Enemy;
        m_rarity = GameRarity.Starter;

        InitializeWithLevel(GetCardLevel());

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Damage;
    }

    public override string GetDesc()
    {
        string description = GetDamageDescString();

        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            description += "Draw a card.";
        }
        
        return description;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.GetHitBySpell(GetSpellValue(), this);

        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            GameHelper.GetPlayer().DrawCard();
        }
    }

    public override void InitializeWithLevel(int level)
    {
        m_cost = 1;
        m_spellEffect = 4;

        if (level >= 1)
        {
            m_spellEffect += 3;
        }
        
        if (level >= 2)
        {
            m_cost = 0;
        }
    }
}
