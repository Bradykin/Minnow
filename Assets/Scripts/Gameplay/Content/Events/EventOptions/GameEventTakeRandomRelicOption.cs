public class GameEventTakeRandomRelicOption : GameEventOption
{
    public GameRelic m_relic;

    private GameRelic m_excludeRelic;

    public GameEventTakeRandomRelicOption(GameRelic excludeRelic = null)
    {
        m_hasTooltip = true;
        m_excludeRelic = excludeRelic;
    }

    public override void Init()
    {
        m_relic = GameRelicFactory.GetRandomRelic(m_excludeRelic);

        m_message = "Take " + m_relic.m_name;
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