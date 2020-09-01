using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrowthCard : GameCardSpellBase
{
    public ContentGrowthCard()
    {
        m_name = "Growth";
        m_desc = "Permanently grow grasslands into a forest!";
        m_playDesc = "Growth transforms the land into a forest";
        m_targetType = Target.Tile;
        m_typeline = "Spell - " + m_targetType;
        m_cost = 2;
        m_icon = UIHelper.GetIconCard(m_name);
        m_rarity = GameRarity.Uncommon;
    }

    public override bool IsValidToPlay(GameTile targetTile)
    {
        if (!(targetTile.GetTerrain() is ContentGrassTerrain))
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

        targetTile.SetTerrain(new ContentForestTerrain());
    }
}
