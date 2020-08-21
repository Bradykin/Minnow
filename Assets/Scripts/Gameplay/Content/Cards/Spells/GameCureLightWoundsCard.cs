using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCureLightWoundsCard : GameCardSpellBase
{
    public GameCureLightWoundsCard()
    {
        m_spellEffect = 5;

        m_name = "Cure Light Wounds";
        m_desc = "Heal a freindly entity for " + GetSpellValue() + " health.";
        m_playDesc = "A stream of healing heals for " + GetSpellValue();
        m_targetType = Target.Ally;
        m_typeline = "Spell - " + m_targetType.ToString();
        m_cost = 1;
        m_icon = UIHelper.GetIconCard(m_name);
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        targetEntity.Heal(GetSpellValue());
    }
}
