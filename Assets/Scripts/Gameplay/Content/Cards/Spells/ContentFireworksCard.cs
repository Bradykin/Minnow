using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFireworksCard : GameCardSpellBase
{
    private int m_range = 2;

    public ContentFireworksCard()
    {
        m_spellEffect = 5;

        m_name = "Fireworks";
        m_targetType = Target.Enemy;
        m_cost = 0;
        m_rarity = GameRarity.Uncommon;
        m_xSpell = true;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.DamageSpell);

        m_onPlaySFX = AudioHelper.LargeImpact;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Deal " + m_spellEffect + mpString + "X damage to target enemy unit and up to X other random enemy units in range " + m_range + ".";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(targetUnit.GetGameTile(), m_range, 1);

        int xVal = GameHelper.GetPlayer().GetXValue();

        int damage = xVal * GetSpellValue();

        base.PlayCard(targetUnit);

        targetUnit.GetHitBySpell(damage, this);

        List<GameUnit> targets = new List<GameUnit>();

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameUnit unit = surroundingTiles[i].GetOccupyingUnit();

            if (unit != null && !unit.m_isDead && unit.GetTeam() == Team.Enemy)
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
            targets[index].GetHitBySpell(damage, this);
            targets.RemoveAt(index);
        }
    }
}