public class GameEventCardSelectOption : GameEventOption
{
    private GameCard m_card;

    public GameEventCardSelectOption(GameCard card)
    {
        m_card = card;

        m_hasTooltip = true;
    }

    public override string GetMessage()
    {
        m_message = "Gain " + m_card.GetName() + ".";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        GameNotificationManager.RecordCardSingleChoice(m_card, true);

        player.AddCardToDiscard(GameCardFactory.GetCardClone(m_card), true);

        EndEvent();
    }

    public override void BuildTooltip()
    {
        if (m_card is GameUnitCard)
        {
            GameUnit unit = ((GameUnitCard)m_card).GetUnit();

            UIHelper.CreateUnitTooltip(unit);
        }
        else
        {
            UIHelper.CreateSpellTooltip(m_card);
        }
    }
}
