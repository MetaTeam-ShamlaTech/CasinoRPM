using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    #region States
    public LoadingState LoadingState = new();
    public LoginState LoginState = new();
    public PreGameState PreGameState = new();
    public PlaceBetState PlaceBetState = new();
    public GameplayState GameplayState = new();
    public PostGameState PostGameState = new();
    #endregion States

    #region Behaviours
    [Header("GameState Behaviours")]
    public Loading loading;
    public Login login;
    public PreGame preGame;
    public PlaceBet placeBet;
    public Gameplay gameplay;
    public PostGame postGame;
    #endregion Behaviours

    #region Managers
    public UIManager uIManager;
    public ChipManager chipManager;
    #endregion Managers

    public GameState currentState { get; private set; }

    public void StartGame()
    {
        Init(PreGameState);
    }

    public void Init(GameState state)
    {
        currentState = state;
        uIManager.UITrigger?.Invoke(currentState, true);
        currentState.StartState(this);
    }

    public void ChangeState(GameState state)
    {
        uIManager.UITrigger?.Invoke(currentState, false);
        currentState = state;
        uIManager.UITrigger?.Invoke(currentState, true);
        currentState.StartState(this);
    }
}