using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFireworksCard : GameCardSpellBase
{
    private int m_range = 3;

    public ContentFireworksCard()
    {
        m_spellEffect = 5;

        m_name = "Fireworks";
        m_targetType = Target.Enemy;
        m_cost = 0;
        m_rarity = GameRarity.Uncommon;
        m_xSpell = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.MagicPower);

        m_audioCategory = AudioHelper.SpellAudioCategory.Damage;
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

        int damage = GameHelper.GetPlayer().GetXValue() * GetSpellValue();

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

        for (int i = 0; i < GameHelper.GetPlayer().GetXValue(); i++)
        {
            if (targets.Count == 0)
            {
                break;
            }

            int index = Random.Range(0, targets.Count);
            targets[index].GetHitBySpell(damage, this);
            targets.RemoveAt(index);
        }

        base.PlayCard(targetUnit);
    }
}