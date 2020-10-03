using System.Collections.Generic;
using UnityEngine;

public class GameEventChemistKnowledgeableSpellpowerOption : GameEventOption
{
    private GameTile m_tile;
    private int m_goldCost;
    private int m_spellpowerIncrease = 1;

    public GameEventChemistKnowledgeableSpellpowerOption(GameTile tile, int goldCost)
    {
        m_tile = tile;
        m_goldCost = goldCost;
    }

    public override string GetMessage()
    {
        m_message = "Spend " + m_goldCost + " gold: Gain Knowledgeable: get " + m_spellpowerIncrease + " spellpower until end of turn.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        if (!m_tile.IsOccupied())
        {
            return;
        }

        if (GameHelper.GetPlayer().m_wallet.m_gold < m_goldCost)
        {
            return;
        }

        m_tile.m_occupyingUnit.AddKeyword(new GameKnowledgeableKeyword(new GameGainTempSpellpowerAction(m_spellpowerIncrease)));
        GameHelper.GetPlayer().m_wallet.SubtractResources(new GameWallet(m_goldCost));

        EndEvent();
    }
}