public class PlaceBetState : GameState
{
    public override void StartState(FiniteStateMachine FiniteStateMachine)
    {
        FiniteStateMachine.placeBet.Init(FiniteStateMachine);
    }
}