public class GameEventLeaveOption : GameEventOption
{
    public GameEventLeaveOption()
    {
        m_message = "Leave";
    }

    public GameEventLeaveOption(string message)
    {
        m_message = message;
    }

    public override void AcceptOption()
    {
        UIEventController.Instance.EndEvent(this);
    }

    //Intentionally left blank
    public override void DeclineOption()
    {

    }
}
