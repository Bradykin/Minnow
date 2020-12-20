using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentProtectionCard : GameCardSpellBase
{
    private int m_range = 3;

    public ContentProtectionCard()
    {
        m_name = "Protection";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Common;
        m_xSpell = true;

        m_cost = 0;

        m_keywordHolder.AddKeyword(new GameDamageShieldKeyword());

        m_tags.AddTag(GameTag.TagType.Tank);
        m_tags.AddTag(GameTag.TagType.DamageShield);

        SetupBasicData();

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        return $"Target allied unit and up to X other allied units within range {m_range} gain <b>Damage Shield</b>.\n";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        int xVal = GameHelper.GetPlayer().GetXValue();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(targetUnit.GetGameTile(), m_range, 1);

        base.PlayCard(targetUnit);

        if (xVal > 0)
        {
            targetUnit.AddKeyword(new GameDamageShieldKeyword(), false, false);

            List<GameUnit> targets = new List<GameUnit>();

            for (int i = 0; i < surroundingTiles.Count; i++)
            {
                GameUnit unit = surroundingTiles[i].GetOccupyingUnit();

                if (unit != null && !unit.m_isDead && unit.GetTeam() == Team.Player)
                {
                    targets.Add(unit);
                }
            }

            for (int i = 0; i < xVal; i++)
            {
                if (targets.Count == 0)
                {
                    break;
                }

                int index = Random.Range(0, targets.Count);
                targets[index].AddKeyword(new GameDamageShieldKeyword(), false, false);
                targets.RemoveAt(index);
            }
        }
    }
}
