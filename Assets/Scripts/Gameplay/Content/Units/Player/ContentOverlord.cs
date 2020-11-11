using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOverlord : GameUnit
{
    public ContentOverlord()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_maxHealth = 12;
        m_maxStamina = 6;
        m_staminaRegen = 3;
        m_power = 2;
        m_staminaToAttack = 1;

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;
        AddKeyword(new GameFlyingKeyword(), false);
        AddKeyword(new GameRangeKeyword(3), false);

        m_name = "Overlord";
        m_desc = "Spends all Stamina to attack, deals damage equal to power times Stamina spent.\n";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override int GetDamageToDealTo(GameUnit target)
    {
        int damage = GetPower() * (GetCurStamina() + m_staminaToAttack);
        this.SpendStamina(GetCurStamina());

        return damage;
    }

    public override int GetDamageToDealTo(GameBuildingBase target)
    {
        int damage = GetPower() * (GetCurStamina() + m_staminaToAttack);
        this.SpendStamina(GetCurStamina());

        return damage;
    }
}