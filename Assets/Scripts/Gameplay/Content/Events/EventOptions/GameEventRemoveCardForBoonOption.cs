public class GameEventRemoveCardForBoonOption : GameEventOption
{
    public override string GetMessage()
    {
        m_message = "NOT YET IMPLEMENTED. Remove a card from your deck. Starter/Common card = no boon, Uncommon card = +2/+5, Rare card = +2 action point regen.";

        return base.GetMessage();
    }

    public override void AcceptOption()
    {
        GamePlayer player = GameHelper.GetPlayer();

        if (player == null)
        {
            return;
        }

        UIDeckViewController.Instance.Init(player.m_deckBase.GetDeck(), UIDeckViewController.DeckViewType.Remove);

        EndEvent();
    }
}