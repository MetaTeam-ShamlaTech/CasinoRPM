using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject LoadingPanel;
    [SerializeField] private GameObject LoginPanel;
    [SerializeField] private GameObject StartGamePanel;
    [SerializeField] private GameObject PlaceBetsPanel;
    [SerializeField] private GameObject PlaceBetsOverlayPanel;
    [SerializeField] private GameObject GameplayPanel;
    [SerializeField] private GameObject NextMatchPanel;

    public Action<GameState, bool> UITrigger;
    #endregion Variables

    private void Awake()
    {
        UITrigger += ToggleUIElements;
    }

    private void ToggleUIElements(GameState state, bool enabled)
    {
        switch (state)
        {
            case LoadingState: LoadingUI(enabled); break;
            case LoginState: LoginUI(enabled); break;
            case PreGameState: StartMatchUI(enabled); break;
            case PlaceBetState: PlaceBetUI(enabled); break;
            case GameplayState: GameplayUI(enabled); break;
            case PostGameState: NextMatchUI(enabled); break;
        }
    }

    private void LoadingUI(bool enabled)
    {
        LoadingPanel.SetActive(enabled);
        LoginPanel.SetActive(false);
        StartGamePanel.SetActive(false);
        PlaceBetsPanel.SetActive(false);
        PlaceBetsOverlayPanel.SetActive(false);
        GameplayPanel.SetActive(false);
        NextMatchPanel.SetActive(false);
    }

    private void LoginUI(bool enabled)
    {
        LoadingPanel.SetActive(false);
        LoginPanel.SetActive(enabled);
        StartGamePanel.SetActive(false);
        PlaceBetsPanel.SetActive(false);
        PlaceBetsOverlayPanel.SetActive(false);
        GameplayPanel.SetActive(false);
        NextMatchPanel.SetActive(false);
    }

    private void StartMatchUI(bool enabled)
    {
        LoadingPanel.SetActive(false);
        LoginPanel.SetActive(false);
        StartGamePanel.SetActive(enabled);
        PlaceBetsPanel.SetActive(false);
        PlaceBetsOverlayPanel.SetActive(false);
        GameplayPanel.SetActive(false);
        NextMatchPanel.SetActive(false);
    }

    private void PlaceBetUI(bool enabled) 
    {
        LoadingPanel.SetActive(false);
        LoginPanel.SetActive(false);
        StartGamePanel.SetActive(false);
        PlaceBetsPanel.SetActive(enabled);
        PlaceBetsOverlayPanel.SetActive(true);
        GameplayPanel.SetActive(false);
        NextMatchPanel.SetActive(false);
    }

    private void GameplayUI(bool enabled)
    {
        LoadingPanel.SetActive(false);
        LoginPanel.SetActive(false);
        StartGamePanel.SetActive(false);
        PlaceBetsPanel.SetActive(false);
        GameplayPanel.SetActive(!enabled);
        NextMatchPanel.SetActive(false);
    }

    private void NextMatchUI(bool enabled)
    {
        LoadingPanel.SetActive(false);
        LoginPanel.SetActive(false);
        StartGamePanel.SetActive(false);
        PlaceBetsPanel.SetActive(false);
        PlaceBetsOverlayPanel.SetActive(false);
        GameplayPanel.SetActive(false);
        NextMatchPanel.SetActive(enabled);
    }
}