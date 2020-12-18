using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOverlord : GameUnit
{
    public ContentOverlord()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Rare;
        AddKeyword(new GameFlyingKeyword(), true, false);
        AddKeyword(new GameRangeKeyword(3), true, false);

        m_name = "Overlord";
        m_desc = "Spends all Stamina to attack, deals damage equal to power times Stamina spent.\n";
        m_typeline = Typeline.Creation;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.LazerAttack;

        LateInit();
    }

    public override int GetDamageToDealTo(GameUnit target)
    {
        int damage = GetPower() * GetCurStamina();

        return damage;
    }

    public override int GetDamageToDealTo(GameBuildingBase target)
    {
        int damage = GetPower() * GetCurStamina();

        return damage;
    }

    public override int GetStaminaToAttack(GameElementBase targetToAttack)
    {
        return Mathf.Max(1, GetCurStamina());
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 12;
        m_maxStamina = 6;
        m_staminaRegen = 3;
        m_power = 4;

        m_staminaToAttack = 1;
    }
}