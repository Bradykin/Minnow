using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFireboltCard : GameCardSpellBase
{
    public ContentFireboltCard()
    {
        m_spellEffect = 3;

        m_name = "Firebolt";
        m_desc = "Blast an enemy for " + GetSpellValue() + " damage.";
        m_playDesc = "A bolt of fire strikes the foe for " + GetSpellValue();
        m_targetType = Target.Enemy;
        m_typeline = "Spell - " + m_targetType;
        m_cost = 1;
        m_icon = UIHelper.GetIconCard(m_name);
        m_rarity = GameRarity.Common;
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        targetEntity.Hit(GetSpellValue());
    }
}
