public class GameEventRemoveCardOption : GameEventOption
{
    public override string GetMessage()
    {
        m_message = "Remove a card from your deck.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        UIDeckViewController.Instance.Init(player.m_deckBase.GetDeck(), UIDeckViewController.DeckViewType.Remove, "Remove a Card");

        EndEvent();
    }
}