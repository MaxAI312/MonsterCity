public class EndLevelState : IState
{
    private readonly UI _uI;

    public EndLevelState(UI uI)
    {
        _uI = uI;
    }

    public void Enter()
    {
        _uI.EndLevelMenu.Show();
    }

    public void Exit()
    {
        _uI.EndLevelMenu.Hide();
    }
}
