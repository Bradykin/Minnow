using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWarriorPriestess : GameUnit
{
    public ContentWarriorPriestess() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;
        m_aoeRange = 2;

        AddKeyword(new GameTauntKeyword(), true, false);

        m_name = "Warrior Priestess";
        m_desc = $"When healed; deal that much damage to all enemy units in range {m_aoeRange}.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.SwordHeavy;

        LateInit();
    }

    public override int Heal(int toHeal, bool showNotification = true)
    {
        int healVal = base.Heal(toHeal, showNotification);

        GameHelper.GetGameController().AddIntermissionLock();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(GetGameTile(), m_aoeRange, 1);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameUnit unit = surroundingTiles[i].GetOccupyingUnit();

            if (unit != null && !unit.m_isDead && unit.GetTeam() == Team.Enemy)
            {
                unit.GetHitByAbility(healVal);
                AudioHelper.PlaySFX(AudioHelper.FireBlast);
            }
        }

        GameHelper.GetGameController().RemoveIntermissionLock();

        return healVal;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 50;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_attack = 5;
    }
}