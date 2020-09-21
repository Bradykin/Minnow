using UnityEngine;

public class GameEventSellGoldForPurpleBeamsOption : GameEventOption
{
    private int m_exchangeRate = 1;
    private int m_maxBeams = 10;

    public override void Init()
    {
        int goldAmount = Mathf.Min(GameHelper.GetPlayer().m_wallet.m_gold, m_exchangeRate * m_maxBeams);
        int beamAmount = goldAmount / m_exchangeRate;

        if (goldAmount % m_exchangeRate != 0)
        {
            beamAmount++;
        }

        m_message = "Sacrifice " + goldAmount + " gold to gain " + beamAmount + " to the purple beam count.";
    }

    public override void AcceptOption()
    {
        int goldAmount = Mathf.Min(GameHelper.GetPlayer().m_wallet.m_gold, m_exchangeRate * m_maxBeams);
        int beamAmount = goldAmount / m_exchangeRate;

        if (goldAmount % m_exchangeRate != 0)
        {
            beamAmount++;
        }

        GameHelper.GetPlayer().m_wallet.SubtractResources(new GameWallet(goldAmount));
        Globals.m_purpleBeamCount += beamAmount;

        EndEvent();
    }
}