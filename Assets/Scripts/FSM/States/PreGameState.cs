public class PreGameState : GameState
{
    public override void StartState(FiniteStateMachine FiniteStateMachine)
    {
        FiniteStateMachine.preGame.Init(FiniteStateMachine);
    }
}