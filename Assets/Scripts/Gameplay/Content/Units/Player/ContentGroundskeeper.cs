using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGroundskeeper : GameUnit
{
    private int m_damageReductionVal = 3;
    private int m_thornsVal = 8;

    public ContentGroundskeeper() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.3f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Groundskeeper";
        m_desc = "When in a forest, gains <b>Damage Reduction</b> " + m_damageReductionVal + ", <b>Thorns</b> " + m_thornsVal + ", and <b>Taunt</b>.\n";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.SlamHeavy;

        AddKeyword(new GameForestwalkKeyword(), true, false);

        LateInit();
    }

    public override GameDamageReductionKeyword GetDamageReductionKeyword()
    {
        GameDamageReductionKeyword toReturn = new GameDamageReductionKeyword(0);

        if (base.GetDamageReductionKeyword() != null)
        {
            toReturn.AddKeyword(base.GetDamageReductionKeyword());
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            if (m_gameTile.GetTerrain().IsForest())
            {
                toReturn.AddKeyword(new GameDamageReductionKeyword(m_damageReductionVal));
            }
        }

        if (toReturn.m_damageReduction == 0)
        {
            toReturn = null;
        }

        return toReturn;
    }

    public override GameThornsKeyword GetThornsKeyword()
    {
        GameThornsKeyword toReturn = new GameThornsKeyword(0);

        if (base.GetThornsKeyword() != null)
        {
            toReturn.AddKeyword(base.GetThornsKeyword());
        }

        if (GameHelper.IsUnitInWorld(this))
        {
            if (m_gameTile.GetTerrain().IsForest())
            {
                toReturn.AddKeyword(new GameThornsKeyword(m_thornsVal));
            }
        }

        if (toReturn.m_thornsDamage == 0)
        {
            toReturn = null;
        }

        return toReturn;
    }

    public override GameTauntKeyword GetTauntKeyword()
    {
        if (GameHelper.IsUnitInWorld(this))
        {
            if (m_gameTile.GetTerrain().IsForest())
            {
                return new GameTauntKeyword();
            }
        }

        return base.GetTauntKeyword();
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 50;
        m_maxStamina = 4;
        m_staminaRegen = 1;
        m_power = 5;
    }
}