using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMinorFireboltCard : GameCardSpellBase
{
    public GameMinorFireboltCard()
    {
        m_spellEffect = 3;

        m_name = "Minor Firebolt";
        m_desc = "Blast an enemy entity for " + GetSpellValue() + " damage.";
        m_icon = null;
    }

    public override void PlayCard(GameEntityBase targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        targetEntity.Hit(GetSpellValue());
    }

    public override bool IsValidToPlay(GameEntityBase targetEntity)
    {
        if (targetEntity.GetTeam() == Team.Player)
        {
            return false;
        }

        return true;
    }
}
