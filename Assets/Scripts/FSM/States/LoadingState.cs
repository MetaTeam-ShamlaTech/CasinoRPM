public class LoadingState : GameState
{
    public override void StartState(FiniteStateMachine FiniteStateMachine)
    {
        FiniteStateMachine.loading.Init(FiniteStateMachine);
    }
}