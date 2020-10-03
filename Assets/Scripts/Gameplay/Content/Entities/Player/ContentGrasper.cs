using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrasper : GameEntity
{
    public ContentGrasper()
    {
        m_maxHealth = 9;
        m_maxStamina = 3;
        m_staminaRegen = 2;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Grasper";
        m_desc = "When this hits a unit, it drains all Stamina from it.";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override int HitEntity(GameEntity other, bool spendStamina = true)
    {
        int damageTaken = base.HitEntity(other, spendStamina);

        this.GainStamina(other.GetCurStamina());
        other.EmptyStamina();

        return damageTaken;
    }
}