using System.Collections;
using UnityEngine;

public class Loading : MonoBehaviour
{
    private FiniteStateMachine finiteStateMachine;
    public void Init(FiniteStateMachine FiniteStateMachine)
    {
        Debug.Log("Loading State");
        finiteStateMachine = FiniteStateMachine;
        StartCoroutine(LoadGame(3f));
    }

    private IEnumerator LoadGame(float loadTime)
    {
        yield return new WaitForSeconds(loadTime);
        finiteStateMachine.ChangeState(finiteStateMachine.LoginState);
    }
}