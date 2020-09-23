public class GameEventTransformCardOption : GameEventOption
{
    private UIDeckViewController.DeckFilterType m_deckFilterType;

    public GameEventTransformCardOption(UIDeckViewController.DeckFilterType deckFilterType)
    {
        m_deckFilterType = deckFilterType;
    }

    public override string GetMessage()
    {
        m_message = "Transform a card in your deck into a random other one.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        UIDeckViewController.Instance.Init(player.m_deckBase.GetDeck(), UIDeckViewController.DeckViewType.Transform, m_deckFilterType, "Transform a card");

        EndEvent();
    }
}