public class GameplayState : GameState
{
    public override void StartState(FiniteStateMachine FiniteStateMachine)
    {
        FiniteStateMachine.gameplay.Init(FiniteStateMachine);
    }
}