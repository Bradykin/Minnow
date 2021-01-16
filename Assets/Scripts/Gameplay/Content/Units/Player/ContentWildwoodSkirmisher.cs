using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWildwoodSkirmisher : GameUnit
{
    private int m_statBoost = 25;

    public ContentWildwoodSkirmisher() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.5f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        AddKeyword(new GameForestwalkKeyword(), true, false);

        m_name = "Wildwood Skirmisher";
        m_desc = $"When in a forest, gain +{m_statBoost}/+0 and '<b>Victorious:</b> Fully heal'.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        LateInit();
    }

    public override AudioClip GetAttackSFX()
    {
        if (GetGameTile() != null && GetGameTile().GetTerrain().IsForest())
        {
            return AudioHelper.SwordHeavy;
        }

        return AudioHelper.SwordLight;
    }

    public override int GetAttack()
    {
        int returnAttack = base.GetAttack();

        if (GameHelper.IsUnitInWorld(this))
        {
            if (m_gameTile.GetTerrain().IsForest())
            {
                returnAttack += m_statBoost;
            }
        }

        return returnAttack;
    }

    public override GameVictoriousKeyword GetVictoriousKeyword()
    {
        GameVictoriousKeyword toReturn = new GameVictoriousKeyword(null);

        if (base.GetVictoriousKeyword() != null)
        {
            toReturn.AddKeyword(base.GetVictoriousKeyword());
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            if (m_gameTile.GetTerrain().IsForest())
            {
                toReturn.AddKeyword(new GameVictoriousKeyword(new GameFullHealAction(this)));
            }
        }

        if (toReturn.IsEmpty())
        {
            toReturn = null;
        }

        return toReturn;
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 15;
        m_maxStamina = 5;
        m_staminaRegen = 4;
        m_attack = 5;
    }
}