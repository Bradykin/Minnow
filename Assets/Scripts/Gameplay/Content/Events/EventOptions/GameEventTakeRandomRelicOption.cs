public class GameEventTakeRandomRelicOption : GameEventOption
{
    public GameRelic m_relic;

    private GameElementBase.GameRarity? m_rarity;
    private GameRelic m_excludeRelic;

    public GameEventTakeRandomRelicOption(GameElementBase.GameRarity? rarity = null, GameRelic excludeRelic = null)
    {
        m_hasTooltip = true;
        m_rarity = rarity;
        m_excludeRelic = excludeRelic;
    }

    public override void Init()
    {
        if (m_rarity == null)
        {
            m_relic = GameRelicFactory.GetRandomRelic(m_excludeRelic);
        }
        else
        {
            m_relic = GameRelicFactory.GetRandomRelicAtRarity(m_rarity.Value, m_excludeRelic);
        }

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