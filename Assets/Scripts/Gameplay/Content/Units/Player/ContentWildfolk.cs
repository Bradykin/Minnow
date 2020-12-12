using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWildfolk : GameUnit
{
    private int m_effectRange = 2;

    public ContentWildfolk()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.3f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;

        m_name = "Wildfolk";
        m_desc = "When an allied <b>Monster</b> unit is summoned within " + m_effectRange + " range, give it <b>Fade</b>.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);

        AddKeyword(new GameFadeKeyword(), true, false);
        AddKeyword(new GameForestwalkKeyword(), true, false);

        LateInit();
    }

    public override void OnOtherSummon(GameUnit other)
    {
        base.OnOtherSummon(other);

        if (other.GetTypeline() == Typeline.Monster)
        {
            int distanceBetween = WorldGridManager.Instance.GetPathLength(GetGameTile(), other.GetGameTile(), true, false, true);
            if (distanceBetween <= m_effectRange)
            {
                other.AddKeyword(new GameFadeKeyword(), false, false);
            }
        }
    }

    protected override void ResetToBase()
    {
        ResetKeywords(true);

        m_maxHealth = 1;
        m_maxStamina = 3;
        m_staminaRegen = 1;
        m_power = 1;
    }
}
