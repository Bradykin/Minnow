using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrasper : GameEntity
{
    public ContentGrasper()
    {
        m_maxHealth = 5;
        m_maxAP = 3;
        m_apRegen = 1;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Grasper";
        m_desc = "When this hits an entity, it drains all AP from it.\nCan hit allies.";
        m_typeline = Typeline.Mystic;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override bool CanHitEntity(GameEntity other)
    {
        if (!IsInRangeOfEntity(other))
        {
            return false;
        }

        if (!HasAPToAttack())
        {
            return false;
        }

        if (other == this)
        {
            return false;
        }

        return true;
    }

    public override int HitEntity(GameEntity other)
    {
        int damageTaken = base.HitEntity(other);

        this.GainAP(other.GetCurAP());
        other.EmptyAP();

        return damageTaken;
    }
}