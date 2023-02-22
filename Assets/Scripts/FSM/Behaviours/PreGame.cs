using System.Collections;
using UnityEngine;

public class PreGame : MonoBehaviour
{
    private FiniteStateMachine finiteStateMachine;
    public void Init(FiniteStateMachine FiniteStateMachine)
    {
        Debug.Log("Start Match State");
        finiteStateMachine = FiniteStateMachine;
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        finiteStateMachine.ChangeState(finiteStateMachine.PlaceBetState);
    }
}