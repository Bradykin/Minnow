using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentAegisCard : GameCardSpellBase
{
    private int m_amount = 1;
    
    public ContentAegisCard()
    {
        m_name = "Aegis";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Starter;

        m_keywordHolder.AddKeyword(new GameDamageShieldKeyword(-1));

        InitializeWithLevel(GetCardLevel());

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        string description = m_desc;

        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            description += "\nDraw a card.";
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

        targetUnit.AddKeyword(new GameDamageShieldKeyword(m_amount), false);

        if (GetCardLevel() >= 2)
        {
            if (targetUnit.GetBleedKeyword() != null)
            {
                targetUnit.SubtractKeyword(targetUnit.GetBleedKeyword());
            }

            if (targetUnit.GetBrittleKeyword() != null)
            {
                targetUnit.SubtractKeyword(targetUnit.GetBrittleKeyword());
            }

            if (targetUnit.GetRootedKeyword() != null)
            {
                targetUnit.RemoveKeyword(targetUnit.GetRootedKeyword());
            }
        }

        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            GameHelper.GetPlayer().DrawCard();
        }
    }

    public override void InitializeWithLevel(int level)
    {
        m_cost = 1;

        if (level >= 1)
        {
            m_amount = 2;
        }

        m_desc = "Give target allied unit " + m_amount + " <b>Damage Shield</b>.\n";

        if (level >= 2)
        {
            m_desc += "Removes all <b>Brittle</b>, <b>Bleed</b>, and <b>Rooted</b> effects from the target.\n";
        }
    }
}
