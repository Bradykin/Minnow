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

        GameNotificationManager.RecordRelicSingleChoice(m_relic, true);

        player.AddRelic(m_relic);

        EndEvent();
    }

    public override void DeclineOption()
    {
        GameNotificationManager.RecordRelicSingleChoice(m_relic, false);
    }

    public override void BuildTooltip()
    {
        UIHelper.CreateRelicTooltip(m_relic);
    }
}