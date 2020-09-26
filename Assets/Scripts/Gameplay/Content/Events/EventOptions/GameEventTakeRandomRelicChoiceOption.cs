public class GameEventTakeRandomRelicChoiceOption : GameEventOption
{
    public GameRelic m_relic1;
    public GameRelic m_relic2;

    private GameElementBase.GameRarity m_rarity;
    private GameRelic m_excludeRelic;

    public GameEventTakeRandomRelicChoiceOption(GameElementBase.GameRarity rarity)
    {
        m_hasTooltip = true;
        m_rarity = rarity;
    }

    public override void Init()
    {
        m_relic1 = GameRelicFactory.GetRandomRelicAtRarity(m_rarity);
        m_relic2 = GameRelicFactory.GetRandomRelicAtRarity(m_rarity, m_relic1);

        m_message = "Take one of two relics.";
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        UIRelicSelectController.Instance.Init(m_relic1, m_relic2);

        EndEvent();
    }
}