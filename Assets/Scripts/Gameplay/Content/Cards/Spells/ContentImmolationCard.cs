using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentImmolationCard : GameCardSpellBase
{
    private int m_amount = 5;
    private int m_multiple = 4;
    
    public ContentImmolationCard()
    {
        m_name = "Immolation";
        m_desc = "Deal " + m_amount + " damage to target enemy. If they are on a non-burned forest tile, multiply that amount by " + m_multiple + " and burn down the forest.";
        m_playDesc = "The target gets drained!";
        m_targetType = Target.Enemy;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();
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
            targetEntity.GetHit(m_amount * m_multiple);
        }
        else
        {
            targetEntity.GetHit(m_amount);
        }
    }
}
