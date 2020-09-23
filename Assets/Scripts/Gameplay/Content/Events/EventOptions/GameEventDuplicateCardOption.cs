public class GameEventDuplicateCardOption : GameEventOption
{
    private UIDeckViewController.DeckFilterType m_deckFilterType;

    public GameEventDuplicateCardOption(UIDeckViewController.DeckFilterType deckFilterType)
    {
        m_deckFilterType = deckFilterType;
    }
    
    public override string GetMessage()
    {
        m_message = "Create a copy of a card in your deck!";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        UIDeckViewController.Instance.Init(player.m_deckBase.GetDeck(), UIDeckViewController.DeckViewType.Duplicate, m_deckFilterType, "Duplicate a Card");

        EndEvent();
    }
}