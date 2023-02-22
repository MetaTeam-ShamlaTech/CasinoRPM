using System.Collections;
using UnityEngine;

public class PlaceBet : MonoBehaviour
{
    #region Variables
    private FiniteStateMachine finiteStateMachine;
    [SerializeField] private GameObject betBtn;
    private bool isrunning;
    private int[] lastBetArray;
    #endregion Variables

    public void Init(FiniteStateMachine FiniteStateMachine)
    {
        Debug.Log("Place Bets State");
        finiteStateMachine = FiniteStateMachine;
        betBtn.SetActive(false);
    }

    public void ChipIn(int value)
    {
        betBtn.SetActive(true);
        finiteStateMachine.chipManager.addChip?.Invoke(value);
    }

    public void ResetBet()
    {
        betBtn.SetActive(false);
        finiteStateMachine.chipManager.destroyChip?.Invoke();
    }

    public void LastBet()
    {
        if (lastBetArray != null)
        {
            foreach (int value in lastBetArray)
                finiteStateMachine.chipManager.addChip?.Invoke(value);
            betBtn.SetActive(true);
        }
    }

    public void BetIn()
    {
        lastBetArray = finiteStateMachine.chipManager.GetChipValueList().ToArray();
        StartCoroutine(PlaceBets());
    }

    private IEnumerator PlaceBets()
    {
        if (!isrunning)
        {
            isrunning = true;
            finiteStateMachine.uIManager.UITrigger?.Invoke(finiteStateMachine.currentState, false);
            finiteStateMachine.chipManager.placeBet?.Invoke();
            yield return new WaitForSeconds(1f);
            finiteStateMachine.ChangeState(finiteStateMachine.GameplayState);
            betBtn.SetActive(false);
            isrunning = false;
            yield return null;
        }
    }
}