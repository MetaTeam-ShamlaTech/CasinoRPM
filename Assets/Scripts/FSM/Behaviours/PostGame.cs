using System.Collections;
using UnityEngine;

public class PostGame : MonoBehaviour
{
    private FiniteStateMachine finiteStateMachine;
    public void Init(FiniteStateMachine FiniteStateMachine)
    {
        Debug.Log("Next Match State");
        finiteStateMachine = FiniteStateMachine;
        StartCoroutine(NextGame());
    }

    private IEnumerator NextGame()
    {
        yield return new WaitForSeconds(3f);
        finiteStateMachine.ChangeState(finiteStateMachine.PreGameState);
    }
}