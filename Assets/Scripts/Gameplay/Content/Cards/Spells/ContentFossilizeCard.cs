using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFossilizeCard : GameCardSpellBase
{
    private int m_powerAmount = 2;
    private int m_staminaDrainAmount = 1;
    private int m_brittleAmount = 3;

    public ContentFossilizeCard()
    {
        m_name = "Fossilize";
        m_desc = "Target enemy unit gets -" + m_powerAmount + "/-0, -" + m_staminaDrainAmount + " Stamina, and gains '<b>Brittle</b>: " + m_brittleAmount + "'.";
        m_targetType = Target.Enemy;
        m_cost = 3;
        m_rarity = GameRarity.Uncommon;

        m_keywordHolder.m_keywords.Add(new GameBrittleKeyword(-1));

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.DamageSpell);
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddPower(-m_powerAmount);
        targetEntity.SpendStamina(m_staminaDrainAmount);

        GameBrittleKeyword brittleKeyword = targetEntity.GetKeyword<GameBrittleKeyword>();
        if (brittleKeyword != null)
        {
            brittleKeyword.IncreaseAmount(m_brittleAmount);
        }
        else
        {
            targetEntity.AddKeyword(new GameBrittleKeyword(m_brittleAmount));
        }
    }
}
