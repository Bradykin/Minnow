using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentImmolationCard : GameCardSpellBase
{
    public ContentImmolationCard()
    {
        m_spellEffect = 5;

        m_name = "Immolation";
        m_targetType = Target.Enemy;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.MagicPower);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.DamageSpell);

        m_onPlaySFX = AudioHelper.FireBlast;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        string startingDesc = GetDamageDescString();
        startingDesc += $"x{UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} damage if the target is on a forest.";

        return startingDesc;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        if (targetUnit.GetGameTile().GetTerrain().IsForest() && targetUnit.GetGameTile().GetTerrain().CanBurn())
        {
            targetUnit.GetGameTile().SetTerrain(GameTerrainFactory.GetBurnedTerrainClone(targetUnit.GetGameTile().GetTerrain()));
            targetUnit.GetHitBySpell(GetSpellValue() * GetSpellValue(), this);
        }
        else
        {
            targetUnit.GetHitBySpell(GetSpellValue(), this);
        }
    }
}
