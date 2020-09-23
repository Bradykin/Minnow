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
        m_message = "Gain " + m_card.m_name + ".";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        player.AddCardToDiscard(GameCardFactory.GetCardClone(m_card), true);

        EndEvent();
    }

    public override void BuildTooltip()
    {
        if (m_card is GameCardEntityBase)
        {
            GameEntity entity = ((GameCardEntityBase)m_card).GetEntity();

            UIHelper.CreateEntityTooltip(entity);
        }
        else
        {
            UIHelper.CreateSpellTooltip(m_card);
        }
    }
}
