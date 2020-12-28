using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWildfolk : GameUnit
{
    public ContentWildfolk() : base()
    {
        m_worldTilePositionAdjustment = new Vector3(0, 0.3f, 0);

        m_team = Team.Player;
        m_rarity = GameRarity.Uncommon;
        m_aoeRange = 2;

        m_name = "Wildfolk";
        m_desc = $"When an allied <b>Monster</b> unit is summoned within {m_aoeRange} range, give it <b>Fade</b>.\n";
        m_typeline = Typeline.Humanoid;
        m_icon = UIHelper.GetIconUnit(m_name);
        m_attackSFX = AudioHelper.PunchLight;

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
            if (distanceBetween <= m_aoeRange)
            {
                other.AddKeyword(new GameFadeKeyword(), false, false);
                AudioHelper.PlaySFX(AudioHelper.SmallBuff);
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
