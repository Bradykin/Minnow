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
        m_typeline = "Spell - Ally";
        m_cost = 1;
        m_icon = null;
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        targetEntity.Heal(GetSpellValue());
    }

    public override bool IsValidToPlay(GameEntity targetEntity)
    {
        if (targetEntity.GetTeam() == Team.Enemy)
        {
            return false;
        }

        return true;
    }
}
