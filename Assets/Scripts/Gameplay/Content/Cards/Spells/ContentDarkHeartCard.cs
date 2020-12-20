using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDarkHeartCard : GameCardSpellBase
{
    private int m_selfDamage = 10;
    private int m_buff = 20;

    public ContentDarkHeartCard()
    {
        m_name = "Dark Heart";
        m_targetType = Target.Unit;
        m_cost = 0;
        m_rarity = GameRarity.Rare;

        m_keywordHolder.AddKeyword(new GameMomentumKeyword(null));

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return "Give a unit +" + m_buff + "/+" + m_buff + ", and '<b>Momentum</b>: Take " + m_selfDamage + " damage.'";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        if (targetUnit.GetTeam() == Team.Player)
        {
            AudioHelper.PlaySFX(AudioHelper.SmallBuff);
        }
        else
        {
            AudioHelper.PlaySFX(AudioHelper.SmallDebuff);
        }

        targetUnit.AddStats(m_buff, m_buff, false, true);
        targetUnit.AddKeyword(new GameMomentumKeyword(new GameGetHitAction(targetUnit, m_selfDamage)), false, false);
    }
}