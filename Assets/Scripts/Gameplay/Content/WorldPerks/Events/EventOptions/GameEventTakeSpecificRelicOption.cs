public class GameEventTakeSpecificRelicOption : GameEventOption
{
    public GameRelic m_relic;

    public GameEventTakeSpecificRelicOption(GameRelic relic)
    {
        m_hasTooltip = true;
        m_relic = relic;
        m_message = "Take " + m_relic.GetName();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.AddRelic(m_relic);

        EndEvent();
    }

    public override void BuildTooltip()
    {
        UIHelper.CreateRelicTooltip(m_relic);
    }
}