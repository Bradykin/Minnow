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
        m_cost = 2;
        m_rarity = GameRarity.Rare;

        m_playerUnlockLevel = 3;

        m_keywordHolder.AddKeyword(new GameBrittleKeyword(-1));

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.DamageSpell);
        m_tags.AddTag(GameTag.TagType.Brittle);

        m_audioCategory = AudioHelper.SpellAudioCategory.Debuff;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.RemoveStats(m_powerAmount, 0);
        targetUnit.SpendStamina(m_staminaDrainAmount);

        targetUnit.AddKeyword(new GameBrittleKeyword(m_brittleAmount), false);
    }
}
