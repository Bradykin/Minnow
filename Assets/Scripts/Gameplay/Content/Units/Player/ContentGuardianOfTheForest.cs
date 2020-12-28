using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGuardianOfTheForest : GameUnit
{
    public ContentGuardianOfTheForest() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(-0.15f, 0, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Common;

        m_name = "Guardian of the Forest";
        m_typeline = Typeline.Monster;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.SlamHeavy;

        AddKeyword(new GameForestwalkKeyword(), true, false);

        LateInit();
    }



    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 30;
        m_maxStamina = 5;
        m_staminaRegen = 3;
        m_power = 8;
    }
}
