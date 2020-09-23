using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentImmolationCard : GameCardSpellBase
{
    private int m_multiple = 4;
    
    public ContentImmolationCard()
    {
        m_spellEffect = 5;

        m_name = "Immolation";
        m_playDesc = "The foe is burned up!";
        m_targetType = Target.Enemy;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string startingDesc = GetDamageDescString();
        startingDesc += "If they are on a non-burned forest tile, multiply that amount by " + m_multiple + " and burn down the forest.";
        //startingDesc += "If they are on a non-burned forest tile, multiply that amount by " + m_multiple + " (" + GetSpellValue() * m_multiple + ") and burn down the forest.";

        return startingDesc;
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        if (targetEntity.GetGameTile().GetTerrain().IsForest() && targetEntity.GetGameTile().GetTerrain().CanBurn())
        {
            targetEntity.GetGameTile().SetTerrain(GameTerrainFactory.GetBurnedTerrainClone(targetEntity.GetGameTile().GetTerrain()));
            targetEntity.GetHit(GetSpellValue() * m_multiple);
        }
        else
        {
            targetEntity.GetHit(GetSpellValue());
        }
    }
}
