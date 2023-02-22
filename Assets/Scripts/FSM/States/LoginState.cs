public class LoginState : GameState
{
    public override void StartState(FiniteStateMachine FiniteStateMachine)
    {
        FiniteStateMachine.login.Init(FiniteStateMachine);
    }
}