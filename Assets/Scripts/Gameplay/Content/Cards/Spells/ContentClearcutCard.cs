using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentClearcutCard : GameCardSpellBase
{
    public ContentClearcutCard()
    {
        m_name = "Clearcut";
        m_desc = "Permanently turn forests back into grasslands!";
        m_playDesc = "You clearcut the forest down, leaving no trace behind.";
        m_targetType = Target.Tile;
        m_cost = 2;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();
    }

    public override bool IsValidToPlay(GameTile targetTile)
    {
        if (!(targetTile.GetTerrain() is ContentForestTerrain))
        {
            return false;
        }

        return base.IsValidToPlay(targetTile);
    }

    public override void PlayCard(GameTile targetTile)
    {
        if (!IsValidToPlay(targetTile))
        {
            return;
        }

        base.PlayCard(targetTile);

        targetTile.SetTerrain(new ContentGrassTerrain());
    }
}
