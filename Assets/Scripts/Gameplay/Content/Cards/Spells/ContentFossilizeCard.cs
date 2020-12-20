using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFossilizeCard : GameCardSpellBase
{
    private int m_powerAmount = 2;
    private int m_staminaDrainAmount = 1;

    public ContentFossilizeCard()
    {
        m_name = "Fossilize";
        m_desc = "Target enemy unit gets -" + m_powerAmount + "/-0, -" + m_staminaDrainAmount + " Stamina, and gains <b>Brittle</b>.";
        m_targetType = Target.Enemy;
        m_cost = 2;
        m_rarity = GameRarity.Rare;

        m_keywordHolder.AddKeyword(new GameBrittleKeyword());

        SetupBasicData();

        m_tagHolder.AddPullTag(GameTagHolder.TagType.UtilitySpell);
        m_tagHolder.AddPullTag(GameTagHolder.TagType.Brittle);

        m_audioCategory = AudioHelper.SpellAudioCategory.Debuff;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.RemoveStats(m_powerAmount, 0, false);
        targetUnit.SpendStamina(m_staminaDrainAmount);

        targetUnit.AddKeyword(new GameBrittleKeyword(), false, false);
    }
}
