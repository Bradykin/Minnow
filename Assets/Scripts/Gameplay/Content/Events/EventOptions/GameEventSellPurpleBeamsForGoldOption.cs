using UnityEngine;

public class GameEventSellPurpleBeamsForGoldOption : GameEventOption
{
    private int m_exchangeRate = 15;
    private int m_maxBeams = 10;

    public override void Init()
    {
        int beamAmount = Mathf.Min(Globals.m_purpleBeamCount, m_maxBeams);
        int goldAmount = beamAmount * m_exchangeRate;

        m_message = "Sacrifice " + beamAmount + " from the purple beam count to gain " + goldAmount + " gold.";
    }

    public override void AcceptOption()
    {
        int beamAmount = Mathf.Min(Globals.m_purpleBeamCount, m_maxBeams);
        int goldAmount = beamAmount * m_exchangeRate;

        Globals.m_purpleBeamCount -= beamAmount;
        GameHelper.GetPlayer().m_wallet.AddResources(new GameWallet(goldAmount));

        EndEvent();
    }
}