using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrasper : GameEntity
{
    public ContentGrasper()
    {
        m_maxHealth = 9;
        m_maxAP = 3;
        m_apRegen = 2;
        m_power = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Grasper";
        m_desc = "When this hits an entity, it drains all AP from it.";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconEntity(m_name);

        LateInit();
    }

    public override int HitEntity(GameEntity other, bool spendAP = true)
    {
        int damageTaken = base.HitEntity(other, spendAP);

        this.GainAP(other.GetCurAP());
        other.EmptyAP();

        return damageTaken;
    }
}