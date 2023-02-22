public class PostGameState : GameState
{
    public override void StartState(FiniteStateMachine FiniteStateMachine)
    {
        FiniteStateMachine.postGame.Init(FiniteStateMachine);
    }
}