using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentImmolationCard : GameCardSpellBase
{
    private int m_multiple = 8;
    
    public ContentImmolationCard()
    {
        m_spellEffect = 5;

        m_name = "Immolation";
        m_targetType = Target.Enemy;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        m_playerUnlockLevel = 1;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Forest);
        m_tags.AddTag(GameTag.TagType.DamageSpell);
        m_tags.AddTag(GameTag.TagType.MagicPower);

        m_audioCategory = AudioHelper.SpellAudioCategory.Damage;
    }

    public override string GetDesc()
    {
        string startingDesc = GetDamageDescString();
        startingDesc += "If they are on a non-burned forest tile, multiply that amount by " + m_multiple + " and burn down the forest.";
        //startingDesc += "If they are on a non-burned forest tile, multiply that amount by " + m_multiple + " (" + GetSpellValue() * m_multiple + ") and burn down the forest.";

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
            targetUnit.GetHit(GetSpellValue() * m_multiple);
        }
        else
        {
            targetUnit.GetHit(GetSpellValue());
        }
    }
}
